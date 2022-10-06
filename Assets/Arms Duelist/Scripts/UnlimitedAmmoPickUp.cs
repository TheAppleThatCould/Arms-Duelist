using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlimitedAmmoPickUp : MonoBehaviour
{
    void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage("ApplyUnlimitedAmmoPickup");
			Destroy(gameObject);
		}
	}
}
