using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Settings")]
public class EnemySO : ScriptableObject
{
    [Header("Shoot Stats")]
    public int EnemyDamage;
    public float EnemySpeed = 100f;
    public float ShootDistance;
    public float LineOfSite;
    public float EnemyFireRate = 1f;
    public float BulletSpeed = 5, BulletLifeTime, Spread, TimeBetweenShots;
    public int BulletsToShoot;
    public GameObject EnemyBullet;

    [Header("Health")]
    public int MaxHP = 20;


    [Header("Misc")]
    public LayerMask WallLayer;
}
