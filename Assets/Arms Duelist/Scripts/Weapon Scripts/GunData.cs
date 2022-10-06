using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : MonoBehaviour {
    [Header("Info")]
    public new string name = "Default";

    [Header("Shooting")]
    public int damage = 20;

    [Header("Reloading")]
    public int bulletNum = 20;
}