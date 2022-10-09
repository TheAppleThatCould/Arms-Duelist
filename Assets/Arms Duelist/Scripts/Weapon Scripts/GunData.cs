using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A script for a gun's data. 
public class GunData : MonoBehaviour {
    // The name of the gun
    [Header("Info")]
    public new string name = "Default";

    // The gun data
    [Header("Gun Data")]
    public int damage = 20;
    public int bulletNum = 20;
    public int totalBulletNum = 20;
    public int ammo = 20;
}