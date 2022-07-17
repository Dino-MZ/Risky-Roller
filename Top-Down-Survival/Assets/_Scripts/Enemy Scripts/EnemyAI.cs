using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    #region Variables

    public bool canMove;

    [SerializeField] private EnemySO enemy;

    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform shootPoint;

    [SerializeField] private GameObject shootHandle;

    [SerializeField] private Transform spritePosition;

    [SerializeField] private AudioSource shootAudio;

    [SerializeField] private AudioClip[] shootSFX;

    [SerializeField] private float minPitch, maxPitch;

    private Transform player;

    private float nextFireTime;

    private float distance;

    private int bulletsShot;

    #endregion


    #region MonoBehaviors

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.speed = enemy.EnemySpeed;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        shootAudio = GameObject.FindGameObjectWithTag("EnemySFX").GetComponent<AudioSource>();
    }

    private void Update()
    {
        distance = Vector2.Distance(player.position, transform.position);

        // Aim at the player
        Vector3 difference = player.position - shootHandle.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        shootHandle.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        // Look right if the player is to the right and vice versa
        if (player.position.x > transform.position.x)
        {
            spritePosition.localScale = new Vector3(1, spritePosition.localScale.y, spritePosition.localScale.z);
        }
        else if (player.position.x < transform.position.x)
        {
            spritePosition.localScale = new Vector3(-1, spritePosition.localScale.y, spritePosition.localScale.z);
        }


        // Move enemy towards the player
        if (distance > enemy.ShootDistance)
        {
            agent.isStopped = false;
            MoveToPlayer();
        }

        // Stop when the enemy is close enough and there are no walls in the way
        else if (distance <= enemy.ShootDistance && !Physics2D.Linecast(transform.position, player.position, enemy.WallLayer))
        {
            agent.isStopped = true;

            if(nextFireTime < Time.time && bulletsShot == 0) 
            {
                nextFireTime = Time.time + enemy.EnemyFireRate;
                bulletsShot = enemy.BulletsToShoot;
                ShootAudio();
                Shoot();
            }  
        }

        else if(distance <= enemy.ShootDistance && Physics2D.Linecast(transform.position, player.position, enemy.WallLayer))
        {
            agent.isStopped = false;
            MoveToPlayer();
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemy.LineOfSite);
        Gizmos.DrawWireSphere(transform.position, enemy.ShootDistance);
    }

    #endregion


    #region My Methods

    private void MoveToPlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Shoot()
    {
        float x = Random.Range(-enemy.Spread, enemy.Spread);
        float y = Random.Range(-enemy.Spread, enemy.Spread);

        Vector2 direction = new Vector3(x, y) + shootPoint.up;

        GameObject t_bullet = EnemyBulletPooler.current.GetPooledObject();

        t_bullet.transform.position = shootPoint.position;
        t_bullet.transform.rotation = shootPoint.rotation;

        t_bullet.SetActive(true);

        t_bullet.GetComponent<EnemyBullet>().Damage = enemy.EnemyDamage;
        t_bullet.GetComponent<EnemyBullet>().Lifetime = enemy.BulletLifeTime;

        Rigidbody2D bulletRb = t_bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(direction * enemy.BulletSpeed, ForceMode2D.Impulse);

        bulletsShot--;

        if(bulletsShot > 0)
        {
            Invoke("Shoot", enemy.TimeBetweenShots);
        }

    }

    void ShootAudio()
    {
        int r = Random.Range(0, shootSFX.Length);
        shootAudio.pitch = Random.Range(minPitch, maxPitch);
        shootAudio.PlayOneShot(shootSFX[r]);
    }

    #endregion
}
