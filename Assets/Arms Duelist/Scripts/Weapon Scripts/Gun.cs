using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Player player;

    float timeSinceLastShot;

    private void Start(){
        player.updateAmmo(gunData.currentAmmo);

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }


    public void StartReload(){
        if(!gunData.reloading){
            StartCoroutine(Reload()); 
        }
    }

    private IEnumerator Reload(){
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;

        player.updateAmmo(gunData.currentAmmo);
    }

    // Check to see if the player is reloading or is in the process of shooting their gun
    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot(){
        // Check to see if player has ammo
        if(gunData.currentAmmo > 0){
            if(CanShoot()){
                if(Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance)){
                    Debug.Log(hitInfo.transform.name);
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                player.updateAmmo(gunData.currentAmmo);
            }
        }
    }

    private void Update(){
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward);
    }
}