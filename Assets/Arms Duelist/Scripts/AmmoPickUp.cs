using UnityEngine;
using System.Collections;

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
