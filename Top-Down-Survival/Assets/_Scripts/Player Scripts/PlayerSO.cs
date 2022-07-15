using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player Data")]
public class PlayerSO : ScriptableObject
{
    [Header("Movement")]
    public float Speed = 5f;
    public float RollLength = 0.5f;
    public float RollCoolDown = 1f;

    [Header("Health")]
    public int MaxHealth = 8;

    [Header("Effects")]
    public float FlashDuration;
    public float HitStopDuration;
}
