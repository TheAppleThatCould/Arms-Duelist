using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
