using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemySO;

    private int currentHP;
    private bool isDead;

    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private AudioSource deathAudio;

    void Start()
    {
        currentHP = enemySO.MaxHP;

        isDead = false;
    }

    void Update()
    {
        if (currentHP <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        damageAudio.Play();
    }

    void Die()
    {
        deathAudio.Play();
        Destroy(gameObject, 0.01f);
    }
}
