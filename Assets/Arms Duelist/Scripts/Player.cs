using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public FirstPersonController firstPersonController;

	public Text currentAmmoText;
	public Text playerHealthText;
    public bool isDead = false;

    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {

        playerHealthText.text = "Health: " + health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        checkIsDead();
    }

    public void updateAmmo(int currentAmmo){
        currentAmmoText.text = "Ammo: " + currentAmmo.ToString();
    }

    public void takeDamage(int damage){
        health -= damage;
        updateHealth();
    }

    public void updateHealth(){
        playerHealthText.text = "Health: " + health.ToString();
    }

    public void checkIsDead(){
        if(health <= 0){
            SceneManager.LoadScene("LoseScene");
            isDead = true;
            firstPersonController.toggleCameraLock();
        }
    }
}
