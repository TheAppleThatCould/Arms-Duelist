using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newPlayer : MonoBehaviour
{

    public float HP = 100;
    private float MaxHP;

    public int damage = 20;
    public int BulletNum = 30;
    public int totalBulletNum;
    public int ammo = 100;

    // Check to see if the player is currently holder a gun.
    public bool isEquiped = false;


    private void Start()
    {
        MaxHP = HP;
        totalBulletNum = BulletNum;
        soue = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Fire();
    }

    public ParticleSystem fx1;
    public ParticleSystem fx2;
    private AudioSource soue;
    public AudioClip shootclip;
    public Text bulletText;


    public void Fire()
    {
        if (Input.GetMouseButtonDown(0)&& BulletNum > 0 && isEquiped == true )
        {   
            fx1.Play();
            fx2.Play();
            soue.PlayOneShot(shootclip);
            BulletNum--;
            bulletText.text = "Bullet:" + BulletNum.ToString() + "/"+ totalBulletNum.ToString();


            Ray ray =   Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
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
            Debug.Log("Out of ammo");
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
            GameManager.instence.overpanel.SetActive(true);Time.timeScale = 0;
        }

        hpslider.value = HP / MaxHP;

    }


    public void Loadmyscene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
