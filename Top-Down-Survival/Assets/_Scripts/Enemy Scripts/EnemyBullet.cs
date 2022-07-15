using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int Damage;
    public float Distance;

    public GameObject particleEffect;

    private void OnEnable()
    {
        Invoke("Disable", Distance);
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
            GameObject t_Effect = Instantiate(particleEffect, transform.position, transform.rotation);
            t_Effect.GetComponent<ParticleSystem>().Play();
            Destroy(t_Effect, 1);
            Disable();
        }
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player HIT!");

            GameObject t_player = collision.gameObject;
            t_player.GetComponent<PlayerHealth>().TakeDamage(Damage);

            GameObject t_Effect = Instantiate(particleEffect, transform.position, transform.rotation);
            t_Effect.GetComponent<ParticleSystem>().Play();
            Destroy(t_Effect, 1);
            Disable();
        }
    }
}
