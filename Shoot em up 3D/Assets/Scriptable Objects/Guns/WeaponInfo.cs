using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfo", menuName = "Confi/WeaponInfo")]


public class WeaponInfo : ScriptableObject
{
    [Header ("Info")]
    public string weaponName;
    public int weaponNumber;

    [Header("Shooting")]
    public int weaponDamage;
    public float maxDistance;

    [Header("Reload")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    public bool reloading;
}
