using UnityEngine;
using System.Collections;

// A script that will send a message to the player to update their ammo
public class AmmoPickUp : MonoBehaviour
{
    public int ammoAmount = 20;

    void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("ApplyAmmoPickup");
			Destroy(gameObject);
		}
	}
}
