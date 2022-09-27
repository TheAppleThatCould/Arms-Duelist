using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : MonoBehaviour {
    [Header("Info")]
    public new string name = "Default";

    [Header("Shooting")]
    public float damage = 5;
    public float maxDistance = 500;

    [Header("Reloading")]

    public int currentAmmo = 20;
    public int magSize = 20;
    public float fireRate = 200;
    public float reloadTime = 1;

    [HideInInspector]
    public bool reloading;
}