using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public EnemySO enemySO;

    private int currentHP;
    private bool isDead;

    private FlashEffect flashEffect;

    [SerializeField] private AudioSource enemyAudio;
    [SerializeField] private AudioClip[] enemySFX;

    void Start()
    {
        currentHP = enemySO.MaxHP;
        flashEffect = gameObject.GetComponent<FlashEffect>();
        isDead = false;

        enemyAudio = GameObject.FindGameObjectWithTag("EnemySFX").GetComponent<AudioSource>();
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
        enemyAudio.PlayOneShot(enemySFX[0]);
    }

    void Die()
    {
        enemyAudio.PlayOneShot(enemySFX[1]);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        currentHP = enemySO.MaxHP;
        isDead = false;
    }
}
