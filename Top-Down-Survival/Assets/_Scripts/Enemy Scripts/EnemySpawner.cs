using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Transform[] spawnpoints;

    [SerializeField] private BasicEnemyPooler basicEnemyPooler;

    [SerializeField] private SpyEnemyPooler spyEnemyPooler;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", 5f, 20f);
        InvokeRepeating("SpawnHarderEnemies", 60f, 60f);
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
                GameObject t_enemy = basicEnemyPooler.GetPooledObject();
                t_enemy.transform.parent = spawnpoints[i].transform;

                t_enemy.SetActive(true);
            }
            else
            {
                GameObject t_enemy2 = spyEnemyPooler.GetPooledObject();
                t_enemy2.transform.parent = spawnpoints[i].transform;

                t_enemy2.SetActive(true);
            }
        }
    }
    void SpawnHarderEnemies()
    {
        if (Pause.isPaused || PlayerHealth.isDead) return;

        for (int i = 0; i < spawnpoints.Length; i++)
        {
            int r = Random.RandomRange(0, 11);

            if (r > 4)
            {
                GameObject t_enemy = basicEnemyPooler.GetPooledObject();
                t_enemy.transform.parent = spawnpoints[i].transform;

                t_enemy.SetActive(true);
            }
            else if (r > 7)
            {
                GameObject t_enemy2 = spyEnemyPooler.GetPooledObject();
                t_enemy2.transform.parent = spawnpoints[i].transform;

                t_enemy2.SetActive(true);
            }
            else 
            {
                GameObject t_enemy = basicEnemyPooler.GetPooledObject();
                GameObject t_enemy2 = spyEnemyPooler.GetPooledObject();

                t_enemy.transform.parent = spawnpoints[i].transform;
                t_enemy2.transform.parent = spawnpoints[i].transform;

                t_enemy.SetActive(true);
                t_enemy2.SetActive(true);
            }
        }
    }
}
