using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun Settings")]
public class GunSO : ScriptableObject
{
    [Header("Gun")]
    public int PlayerDamage;
    public float bulletSpeed, bulletLifeTime, knockbackPower, timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    [Header("Audio")]
    public float minPitch, maxPitch;

    [Header("Camera Shaking")]
    public bool canShake;
    public float magnitude, roughness, fadeIn, fadeOut;
}
