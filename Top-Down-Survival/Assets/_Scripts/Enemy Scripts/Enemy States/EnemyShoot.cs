using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public EnemySO enemySO;
    public Transform player;
    public Transform shootPoint;

    private bool playerInShootDistance;
    private bool viewBlocked;
    private float nextFireTime;
    private Vector3 direction;

    public AudioSource shootAudoio;

    private void Update()
    {
        playerInShootDistance = Physics2D.OverlapCircle(shootPoint.position, enemySO.ShootDistance, enemySO.PlayerLayer);


        // Aim at the player
        direction = player.position - transform.position;
        transform.right = direction;


        if (playerInShootDistance)
        {
            viewBlocked = Physics2D.Linecast(transform.position, player.position, enemySO.GroundLayer);
            if (!viewBlocked && nextFireTime < Time.time)
            {
                nextFireTime = Time.time + enemySO.EnemyFireRate;
                Shoot();
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemySO.ShootDistance);
    }

    private void Shoot()
    {
        shootAudoio.Play();

        GameObject t_bullet = EnemyBulletPooler.current.GetPooledObject();

        t_bullet.transform.position = shootPoint.transform.position;
        t_bullet.transform.rotation = shootPoint.transform.rotation;

        t_bullet.SetActive(true);

        t_bullet.GetComponent<EnemyBullet>().Damage = enemySO.EnemyDamage;

        Rigidbody2D bulletRb = t_bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(shootPoint.transform.up * enemySO.BulletSpeed, ForceMode2D.Impulse);

    }
}
