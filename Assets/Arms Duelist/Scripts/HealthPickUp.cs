using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
