using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;
    public string weaponType;
    public bool isHitScan;
    public bool isProjectile;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;
    public float bulletForce;
    public GameObject projectile;
    [Header("Audio")]
    public AudioClip gunShotFX;
    public AudioClip gunReloadFX;
    public AudioClip gunReadyFX;

    [Header("VFX")]
    public GameObject muzzleFlash;
    
    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    public bool reloading;

    [Header("Augments")]
    public bool hasStagger;

    public void Awake()
    {
        //Set Current Ammo to mag size
        currentAmmo = magSize;
        //Set is reloading to false
        reloading = false;
    }
    
}
