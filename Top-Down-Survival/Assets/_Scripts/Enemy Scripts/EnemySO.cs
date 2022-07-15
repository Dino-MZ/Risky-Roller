using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Settings")]
public class EnemySO : ScriptableObject
{
    [Header("Stats")]
    public int EnemyDamage;
    public float Speed = 100f;
    public float ChaseSpeed = 200f;

    [Header("Health")]
    public int MaxHP = 20;

    [Header("Checks")]
    public float ActivateDistance = 15f;
    public float AttackDistance = 1.5f;
    public Vector2 ActivateVector;
    public Vector2 AttackVector;
    [Space(2)]
    public float StopDistance = 2f;
    public float GroundCheckSize = 0.2f;
    public float PatrolDetectionCheckSize;

    [Header("Custom Behavior")]
    public bool UseOverlapShere = true;

    [Header("Shooter Type")]
    public float ShootDistance = 4;
    public float EnemyFireRate = 2f;
    public float BulletSpeed = 5;
    public GameObject EnemyBullet;


    [Header("Layers")]
    public LayerMask PatrolLayer;
    public LayerMask PlayerLayer;
    public LayerMask GroundLayer;
}
