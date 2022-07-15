using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemySO;

    private int curHP;
    private bool isDead;

    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private AudioSource deathAudio;

    void Start()
    {
        curHP = enemySO.MaxHP;

        isDead = false;
    }

    void Update()
    {
        if (curHP <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }

    public void TakeDamage(int damage)
    {
        curHP -= damage;

        damageAudio.Play();
    }

    void Die()
    {
        deathAudio.Play();
        Destroy(gameObject, 0.01f);
    }
}
