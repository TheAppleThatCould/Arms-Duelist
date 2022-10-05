using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newPlayer : MonoBehaviour
{

    public float HP = 100;
    private float MaxHP;


    public float BulletNum = 30;
    private void Start()
    {
        MaxHP = HP;
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
        if (Input.GetMouseButtonDown(0)&&BulletNum>0)
        {   
            fx1.Play();
            fx2.Play();
            soue.PlayOneShot(shootclip);
            BulletNum--;
            bulletText.text = "Bullet:" + BulletNum.ToString() + "/30";


            Ray ray =   Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,Screen.height/2));
            RaycastHit hitinfo;
            if (Physics.Raycast(ray, out hitinfo)&&hitinfo.collider.tag=="enemy")
            {   
                hitinfo.collider.GetComponent<SoulBoss>().TakeDamage(20);
            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            BulletNum = 30;
            bulletText.text = "Bullet:" + BulletNum.ToString() + "/30"; ;
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
