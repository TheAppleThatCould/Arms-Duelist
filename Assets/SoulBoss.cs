using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class SoulBoss : MonoBehaviour
{
    // Get the remaining enemies from gameManager
    private GameManager gameManager;
    // Get the items asset to spawn on enemy death
    private GameObject ammoPack;
    private GameObject health;

    // Get the first person controller for the purpose of unlocking the mouse lock.
    FirstPersonController firstPersonController;

    private Transform playerPos;
    private NavMeshAgent navAi;
    private Animator animator;
    public float fitDistance = 2;
    public float speed = 2;
    public float timer = 3;
    private float time = 3;

    private AudioSource source;
    private newPlayer player;

    public float HP = 100;
    private float maxhp;

    // Check if enemy is dead
    private bool isDead = false;

    void Start()
    {
        maxhp = HP;
        time = timer;
        source = GetComponent<AudioSource>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<newPlayer>();
        navAi = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Get the game manager instance
        GameObject gameManagerObject = GameObject.Find("/GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        // Get the item assets to copy.
        ammoPack = GameObject.Find("/PotentialPowerUps/AmmoBox");
        health = GameObject.Find("/PotentialPowerUps/Health");
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) { 
            // Run on enemy death ->
            navAi.isStopped = true; 

            if(!isDead){
                // Decrement remaining enemies only once for each enemy.  
                gameManager.enemiesRemaining -= 1;
                isDead = true;
                spawnRandomItem();
            }
            return; 
        }



        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= fitDistance)
        {
            navAi.isStopped = true;


            timer += Time.deltaTime;
            if (timer > time)
            {
                int rand = Random.Range(1, 3);
                animator.SetTrigger("Attack1");
                timer = 0;
            }
            else
            {
                animator.SetBool("Walk", false);
            }

        }
        else
        {
            navAi.isStopped = false;
            navAi.SetDestination(playerPos.position);
              
            animator.SetBool("Walk", true);
        }
    }



    public void Atkclick()
    {
        if (isBoss)
        {
            player.TakeDamage(30);
        }
        else
        {
            player.TakeDamage(10);
        }
    }

    public Slider hpslider;
    public bool isBoss = false;

    public void TakeDamage(float damage)
    {
        if (HP <=0) return;


        HP -= damage;

        if (HP<=0)
        {
            HP = 0;
            navAi.isStopped = true;
            animator.SetTrigger("die");
            GameManager.instence.AddNum();
            hpslider.gameObject.SetActive(false);

            if (isBoss){
       
                Invoke("yanshiying",5); 
            }

            Destroy(this.gameObject,5);
        }

        hpslider.value = HP / maxhp;
    }

    public void yanshiying()
    {
        Time.timeScale = 0;
        
        GameObject player = GameObject.Find("/Player");
        firstPersonController = player.GetComponent<FirstPersonController>();
        firstPersonController.unlockMouseLock();
        SceneManager.LoadScene("WinScene");

    }


    public void spawnRandomItem(){
        // a function that will spawn a random item on enemy death.
        int randomNum = Random.Range(0, 100);
        Debug.Log("Random number: " + randomNum);

        if(randomNum <= 50){
            // 50% chance to drop ammo
            Instantiate(ammoPack, transform.position, ammoPack.transform.rotation);
        } else{
            // 40% chance to drop health
            // No item will drop if the number is above 90.
            if(randomNum > 50 && randomNum <= 90>){
                Instantiate(health, transform.position, ammoPack.transform.rotation);
            }
        }
    }
}
