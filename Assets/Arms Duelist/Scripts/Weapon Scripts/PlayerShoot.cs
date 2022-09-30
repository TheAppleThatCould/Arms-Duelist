using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;
    public Gun gunScript;

    [SerializeField] private KeyCode reloadKey;
    [SerializeField] private AudioClip playerShootGun;           // the sound played when character touches back on ground.

    private void Update(){
        if (Input.GetMouseButton(0)){
            shootInput?.Invoke();
        }

        if(Input.GetKeyDown(reloadKey)){
            reloadInput?.Invoke();
        }


        // playerShootGun.Play();

    }
}
