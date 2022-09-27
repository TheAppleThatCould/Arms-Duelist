using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    // [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Player player;

    public static GunData gunData;

    float timeSinceLastShot;

    private static bool pickedUp = false;

    private void Start(){
        gunData = new GunData();
        if(transform.parent.parent.name == "WeaponHolder"){
            gunData = gameObject.GetComponent<GunData>();
            pickedUp = true;
        }

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        player.updateAmmo(gunData.currentAmmo);

    }

    private void Update(){

        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(muzzle.position, muzzle.forward);

        if(transform.parent.parent.name == "WeaponHolder"){
            gunData = gameObject.GetComponent<GunData>();

        }

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
        Debug.Log("THIS IS the ammo: " + gunData.currentAmmo );
        Debug.Log("This is the name: " + gameObject.name );
        // Check to see if player has ammo
        if(gunData.currentAmmo > 0){
            // Debug.Log("if(gunData.currentAmmo > 0){:  ccurrent ammo:" + gunData.currentAmmo );
            if(CanShoot()){
                // Debug.Log("if(CanShoot()){:  ccurrent ammo:" + gunData.currentAmmo );

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

}
