using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector]
    public int Damage;
    [HideInInspector]
    public float Lifetime;

    public GameObject particleEffect;

    private void OnEnable()
    {
        Invoke("Disable", Lifetime);
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
        if (collision.CompareTag("PlayerHitbox"))
        {
            Debug.Log("Player HIT!");

            GameObject t_player = collision.gameObject;
            t_player.GetComponentInParent<PlayerHealth>().TakeDamage(Damage);

            GameObject t_Effect = Instantiate(particleEffect, transform.position, transform.rotation);
            t_Effect.GetComponent<ParticleSystem>().Play();
            Destroy(t_Effect, 1);
            Disable();
        }
    }
}
