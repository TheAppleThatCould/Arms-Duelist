using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script that will send a message to the player to update their Health
public class HealthPickUp : MonoBehaviour
{
    public int healthAmount = 20;

    void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("ApplyHealthPickup");
			Destroy(gameObject);
		}
	}
}
