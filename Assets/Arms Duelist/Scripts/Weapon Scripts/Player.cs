using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public Text currentAmmoText;

    public int health = 100;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateAmmo(int currentAmmo){
        currentAmmoText.text = "Ammo: " + currentAmmo.ToString();
    }
}
