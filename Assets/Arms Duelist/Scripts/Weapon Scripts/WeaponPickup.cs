using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // get the player 
    private newPlayer playerScript;
    private GunData gunData;

    private GameObject crossHair;

    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, WeaponHolder, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;


    private void Start(){
        // Get the player game object
        playerScript = player.GetComponent<newPlayer>();

        // Get the current gun data.
        gunData = gameObject.GetComponent<GunData>();

        Debug.Log(gunData.damage);
        // Intialize the gunData 
        playerScript.damage = gunData.damage;
        playerScript.BulletNum = gunData.bulletNum;
        playerScript.isEquiped = true;

        // Get the gun crosshair
        crossHair = GameObject.Find("/Canvas/corsirhair");

        if(!equipped){
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if(equipped){
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update(){
        Vector3 distanceToPlayer = player.position - transform.position;
        // pick up weapon
        if(!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull){
            PickUp();

            playerScript.damage = gunData.damage;
            playerScript.BulletNum = gunData.bulletNum;
        }

        // Drop weapon
        if(equipped && Input.GetKeyDown(KeyCode.Q)){
            Drop();
        }
    }

    private void PickUp(){
        equipped = true;
        slotFull = true;
        crossHair.SetActive(true);
        playerScript.isEquiped = true;  

        // Pick up gun and place it in the WeaponHolder and set the position and rotation to zero.
        // This is to let the parent object aka the WeaponHolder set the postion and roation. 
        transform.SetParent(WeaponHolder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;
    }

    private void Drop(){
        equipped = false;
        slotFull = false;
        playerScript.isEquiped = false;
        crossHair.SetActive(false);

        // Remove weapon from WeaponHolder object.
        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        //add random rotation on throw
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random)*10);
    }

}
