using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class newPlayer : MonoBehaviour
{
    // Get the first person controller for the purpose of unlocking the mouse lock.
    private FirstPersonController firstPersonController;

    public float HP = 100;
    private float MaxHP;

    public int damage = 20;
    public int BulletNum = 30;
    public int totalBulletNum;
    public int ammo = 20;

    // Check to see if the player is currently holder a gun.
    public bool isEquiped = false;

    public float powerUpTimer = 0;
    public bool unlimitedAmmo = false;

    private void Start()
    {
        MaxHP = HP;
        totalBulletNum = BulletNum;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Fire();
        // Start to decrement the timer for power up duration.
        if (powerUpTimer >= 0)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer < 1) unlimitedAmmo = false;
        }
    }


    // sound variable
    public ParticleSystem fx1;
    public ParticleSystem fx2;
    private AudioSource source;
    public AudioClip shootclip;
    public Text bulletText;
    public AudioClip pickUpSound;
    public AudioClip loseSound;


    public void Fire()
    {
        if (Input.GetMouseButtonDown(0) && BulletNum > 0 && isEquiped == true)
        {
            fx1.Play();
            fx2.Play();
            source.PlayOneShot(shootclip);

            // Check if the player currently have unlimited ammo power up
            if (!unlimitedAmmo) BulletNum--;

            bulletText.text = "Bullet:" + BulletNum.ToString() + "/" + totalBulletNum.ToString();

            var ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitinfo;

            if (Physics.Raycast(ray, out hitinfo) && hitinfo.collider.tag == "enemy")
                hitinfo.collider.GetComponent<SoulBoss>().TakeDamage(damage);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // If the ammo is more the the total magazine then completely reload the gun else just fill up the gun with the remaining ammo.
            if (ammo >= totalBulletNum)
            {
                // Decrement the amount of ammo it takes to reload the gun
                ammo -= totalBulletNum;
                // Asign the totalBullet the gun can hold to the bullet number
                BulletNum = totalBulletNum;
            }
            else if (ammo > 0)
            {
                BulletNum = BulletNum + ammo;
                ammo = 0;
            }

            bulletText.text = "Bullet:" + BulletNum.ToString() + "/" + totalBulletNum.ToString();
        }
        else
        {
            // Debug.Log("Out of ammo");
        }
    }

    public Slider hpslider;

    public void TakeDamage(float damage)
    {
        if (HP <= 0) return;

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            Time.timeScale = 0;
            // TODO: the game can't restart in loseScene.
            source.PlayOneShot(loseSound);

            firstPersonController = gameObject.GetComponent<FirstPersonController>();
            firstPersonController.unlockMouseLock();
            SceneManager.LoadScene("LoseScene");
            Time.timeScale = 0;
        }

        hpslider.value = HP / MaxHP;
    }


    public void Loadmyscene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Increment ammo when player picks up ammo
    public void ApplyAmmoPickup()
    {
        source.PlayOneShot(pickUpSound);
        ammo = ammo + 20;
    }

    // Increment health when player picks up health
    public void ApplyHealthPickup()
    {
        source.PlayOneShot(pickUpSound);
        HP = HP + 20;
        hpslider.value = HP / MaxHP;
    }

    // A function that will set the bulletNum to -1 inorder to let the player have unlimited ammo
    public void ApplyUnlimitedAmmoPickup()
    {
        // Increment the ammo by 1 to be fair.
        BulletNum = BulletNum + 1;
        // Set the power up timer
        powerUpTimer = 5;
        // and set the unlimitedAmmo variable to true
        unlimitedAmmo = true;
    }
}