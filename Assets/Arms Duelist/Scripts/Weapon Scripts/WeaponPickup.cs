using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // Get scripts 
    private newPlayer playerScript;
    private GunData gunData;
    private GameManager gameManagerScript;

    // Get game objects
    private GameObject weaponHolder;
    private GameObject crossHair;

    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, WeaponHolder, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;


    private Vector3 weaponVectorPosition = new Vector3(0f, 0f, 0f);
    private Vector3 weaponVectorRotation = new Vector3(0f, 0f, 0f);
    private Vector3 weaponVectorScale = new Vector3(1f, 1f, 1f);


    private void Start(){
        // Get the gameManager game object
        GameObject gameManager = GameObject.Find("/GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        // Get the player game object
        playerScript = player.GetComponent<newPlayer>();

        // Get the current gun data.
        weaponHolder = GameObject.Find("/Player/FirstPersonCharacter/WeaponHolder");
        gunData = weaponHolder.transform.GetChild(0).GetComponent<GunData>();


        // Intialize the gunData 
        playerScript.damage = gunData.damage;
        playerScript.BulletNum = gunData.bulletNum;
        playerScript.totalBulletNum = gunData.bulletNum;
        // The isEquiped
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


        // change the position and rotation of a weapon depending on the weapon.
        if(gameObject.name == "Revolver"){
            weaponVectorPosition = new Vector3(-0.9f, 0.15f, -0.325f);
            weaponVectorRotation = new Vector3(0f, 89f, 0f);
            weaponVectorScale = new Vector3(2.5f, 2.5f, 2.5f);
        } else if(gameObject.name == "AssualtRife"){
            weaponVectorPosition = new Vector3(-0.2f, 0f, -0.3f);
            weaponVectorRotation = new Vector3(0f, -90f, 0f);
            weaponVectorScale = new Vector3(4f, 4f, 4f);
        }
        
        transform.localPosition = weaponVectorPosition;
        transform.localRotation = Quaternion.Euler(weaponVectorRotation);
        transform.localScale = weaponVectorScale;
        

        rb.isKinematic = true;
        coll.isTrigger = true;

        // Get the current gun data.
        gunData = weaponHolder.transform.GetChild(0).GetComponent<GunData>();

        playerScript.damage = gunData.damage;
        playerScript.BulletNum = gunData.bulletNum;
        playerScript.totalBulletNum = gunData.totalBulletNum;
        playerScript.ammo = gunData.ammo;

        // update the UI text
        gameManagerScript.updateUIText();
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

        // update the UI text
        gameManagerScript.updateUIText();
    }

}
