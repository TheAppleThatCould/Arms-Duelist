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

    // Default position, rotation and scale.
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


        // Intialize the gunData to the player's weapon variables.
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

    // A function that will pick up a weapon and placed it within the WeaponHolder gameobject
    private void PickUp(){
        // Set certain variable to true when the weapon is equipped.
        equipped = true;
        slotFull = true;
        // Make the crosshair appear once the weapon is picked up.
        crossHair.SetActive(true);
        // Enable the player ability to shoot the gun
        playerScript.isEquiped = true;  

        // Pick up gun and place it in the WeaponHolder and set the position and rotation to zero.
        // This is to let the parent object aka the WeaponHolder set the postion and roation. 
        transform.SetParent(WeaponHolder);


        // Change the position and rotation of a weapon depending on the weapon.
        if(gameObject.name == "Revolver(Clone)"){
            weaponVectorPosition = new Vector3(-0.9f, 0.15f, -0.325f);
            weaponVectorRotation = new Vector3(0f, 89f, 0f);
            weaponVectorScale = new Vector3(2.5f, 2.5f, 2.5f);
        } else if(gameObject.name == "AssualtRife(Clone)"){
            weaponVectorPosition = new Vector3(-0.2f, 0f, -0.3f);
            weaponVectorRotation = new Vector3(0f, -90f, 0f);
            weaponVectorScale = new Vector3(4f, 4f, 4f);
        }else if(gameObject.name == "Pistol"){
            weaponVectorPosition = new Vector3(-0.8f, 0.22f, -0.17f);
            weaponVectorRotation = new Vector3(0f, 0f, 0f);
            weaponVectorScale = new Vector3(1f, 1f, 1f);
        }
        
        // Assign the weapon position, rotation, and scale based on the above three variable inside of the if statement.
        transform.localPosition = weaponVectorPosition;
        transform.localRotation = Quaternion.Euler(weaponVectorRotation);
        transform.localScale = weaponVectorScale;
        
        
        rb.isKinematic = true;
        // Allow the collider to be triggered.
        coll.isTrigger = true;

        // Get the current gun data.
        gunData = weaponHolder.transform.GetChild(0).GetComponent<GunData>();

        // Set the player's weapon variable based on the gun that was picked up.
        playerScript.damage = gunData.damage;
        playerScript.BulletNum = gunData.bulletNum;
        playerScript.totalBulletNum = gunData.totalBulletNum;
        playerScript.ammo = gunData.ammo;

        // update the UI text
        gameManagerScript.updateUIText();
    }

    // A function that will drop the weapon within the WeaponHolder gameobject
    private void Drop(){
        // Set certain variable to false when the weapon isn't equipped.
        equipped = false;
        slotFull = false;
        // Display the shooting functionality when the player doesn't have a weapon
        playerScript.isEquiped = false;
        // Makes the crosshair image disapear if the player doesn't have a weapon
        crossHair.SetActive(false);

        // Remove weapon from WeaponHolder object.
        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;
        
        // Asign the force for the weapon drop. This lets the player "throw" the weapon away
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        // Add random rotation on throw
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random)*10);

        // Update the UI text
        gameManagerScript.updateUIText();
    }

}
