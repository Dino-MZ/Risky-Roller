using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player Data")]
public class PlayerSO : ScriptableObject
{
    [Header("Movement")]
    public float speed;

    [Header("Health")]
    public int maxHealth;

    [Header("Effects")]
    public float flashDuration;
    public float hitStopDuration;
}
