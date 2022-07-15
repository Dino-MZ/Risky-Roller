using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int Damage;

    public GameObject particleEffect;

    public float LifeTime;

    private void OnEnable()
    {
        Invoke("Disable", LifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {

            GameObject t_effect = Instantiate(particleEffect, transform.position, transform.rotation);
            t_effect.GetComponent<ParticleSystem>().Play();

            Destroy(t_effect, 1);
            Disable();
        }
        if (collision.CompareTag("Enemy") || collision.CompareTag("Enemy2") || collision.CompareTag("Enemy3"))
        {
            GameObject t_Enemy = collision.gameObject;
            t_Enemy.GetComponent<EnemyHealth>().TakeDamage(Damage);

            GameObject t_effect = Instantiate(particleEffect, transform.position, transform.rotation);
            t_effect.GetComponent<ParticleSystem>().Play();

            Destroy(t_effect, 1);

            Disable();
        }
        if (collision.CompareTag("Boss"))
        {
            GameObject t_Enemy = collision.gameObject;
            //t_Enemy.GetComponent<BossHealth>().TakeDamage(Damage);

            GameObject t_effect = Instantiate(particleEffect, transform.position, transform.rotation);
            t_effect.GetComponent<ParticleSystem>().Play();

            Destroy(t_effect, 1);

            Disable();
        }
    }
}
