using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        isDead();
    }

    public void updateAmmo(int currentAmmo){
        currentAmmoText.text = "Ammo: " + currentAmmo.ToString();
    }

    public void isDead(){
        if(health <0){
            SceneManager.LoadScene("LoseScene");
        }
    }
}
