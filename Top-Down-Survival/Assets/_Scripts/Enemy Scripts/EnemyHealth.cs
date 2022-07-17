using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemySO;

    private int currentHP;
    private bool isDead;

    private FlashEffect flashEffect;

    // [SerializeField] private AudioSource damageAudio;
    // [SerializeField] private AudioSource deathAudio;

    void Start()
    {
        currentHP = enemySO.MaxHP;
        flashEffect = gameObject.GetComponent<FlashEffect>();
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
        flashEffect.Flash();
        // damageAudio.Play();
    }

    void Die()
    {
        // deathAudio.Play();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        currentHP = enemySO.MaxHP;
        isDead = false;
    }
}
