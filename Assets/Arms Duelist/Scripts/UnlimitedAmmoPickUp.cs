using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script that will send a message to the player to make them have unlimited ammo
public class UnlimitedAmmoPickUp : MonoBehaviour
{
    void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("ApplyUnlimitedAmmoPickup");
			Destroy(gameObject);
		}
	}
}
