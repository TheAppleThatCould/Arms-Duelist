using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class newPlayer : MonoBehaviour
{
    // Get the first person controller for the purpose of unlocking the mouse lock.
    FirstPersonController firstPersonController;

    public float HP = 100;
    private float MaxHP;

    public int damage = 20;
    public int BulletNum = 30;
    public int totalBulletNum;
    public int ammo = 20;

    // Check to see if the player is currently holder a gun.
    public bool isEquiped = false;

    private void Start()
    {
        MaxHP = HP;
        totalBulletNum = BulletNum;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Fire();
    }
    // sound variable
    public ParticleSystem fx1;
    public ParticleSystem fx2;
    private AudioSource source;
    public AudioClip shootclip;
    public Text bulletText;
    public AudioClip pickUpSound;

    public void Fire()
    {
        if (Input.GetMouseButtonDown(0)&& BulletNum > 0 && isEquiped == true )
        {   
            fx1.Play();
            fx2.Play();
            source.PlayOneShot(shootclip);
            BulletNum--;
            bulletText.text = "Bullet:" + BulletNum.ToString() + "/"+ totalBulletNum.ToString();

            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo)&&hitinfo.collider.tag=="enemy")
            {   
                hitinfo.collider.GetComponent<SoulBoss>().TakeDamage(damage);
            }

        }

        if (Input.GetKeyDown(KeyCode.R) && ammo > 0)
        {
            // Decrement the amount of ammo it takes to reload the gun
            ammo -= totalBulletNum;
            // Asign the totalBullet the gun can hold to the bullet number
            BulletNum = totalBulletNum;
            
            bulletText.text = "Bullet:" + BulletNum.ToString() + "/" + totalBulletNum.ToString();
        } else {
            // Debug.Log("Out of ammo");
        }
    }

    public Slider hpslider;
    public void TakeDamage(float damage)
    {
        if (HP <= 0) return;

        HP -= damage;
        if (HP<=0)
        {
            HP = 0;
            Time.timeScale = 0;
            // TODO: the game can't restart in loseScene.
            firstPersonController = gameObject.GetComponent<FirstPersonController>();
            firstPersonController.unlockMouseLock();
            SceneManager.LoadScene("LoseScene");
        }

        hpslider.value = HP / MaxHP;
    }


    public void Loadmyscene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	// Increment ammo when player picks up ammo
	public void ApplyAmmoPickup() {
        source.PlayOneShot(pickUpSound);
		ammo = ammo + 20;
	}

    // Increment health when player picks up health
	public void ApplyHealthPickup() {
        source.PlayOneShot(pickUpSound);
		HP = HP + 20;
        hpslider.value = HP / MaxHP;
	}
}
