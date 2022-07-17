using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Transform[] spawnpoints;

    [SerializeField] private GameObject basicEnemy;

    [SerializeField] private GameObject spyEnemy;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", 15f, 25f);
    }

    void SpawnEnemies()
    {
        if (Pause.isPaused || PlayerHealth.isDead) return;

        for (int i = 0; i < spawnpoints.Length; i++)
        {
            int r = Random.RandomRange(0, 11);

            if (r == 0) return;

            if(r > 8)
            {
                GameObject t_enemy = Instantiate(basicEnemy,spawnpoints[i].position, spawnpoints[i].rotation);
            }
            else
            {
                GameObject t_enemy = Instantiate(spyEnemy, spawnpoints[i].position, spawnpoints[i].rotation);
            }
        }
    }
}
