using UnityEngine;
using System.Collections;

public class AmmoPickUp : MonoBehaviour
{
    public int ammoAmount = 20;

    void OnTriggerEnter(Collider col) {
        Debug.Log("The collider tag: " + col.gameObject.tag);

		if (col.gameObject.tag == "Player") {
            Debug.Log("Sending pick up ammo");
			col.gameObject.SendMessage("ApplyAmmoPickup");
			Destroy(gameObject);
		}
	}
}
