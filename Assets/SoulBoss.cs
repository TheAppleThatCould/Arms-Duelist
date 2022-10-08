using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class SoulBoss : MonoBehaviour
{
    // Get the remaining enemies from gameManager
    private GameManager gameManager;

    // Get the items asset to spawn on enemy death
    private GameObject ammoPack;
    private GameObject health;
    private GameObject unlimitedAmmo;
    // Get the weapon to spawn on enemy death
    private GameObject assualtRife;
    private GameObject revolver;
    private GameObject M1A1;
    private GameObject stenmk2;


    // Get the first person controller for the purpose of unlocking the mouse lock.
    private FirstPersonController firstPersonController;

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

    private void Start()
    {
        maxhp = HP;
        time = timer;
        source = GetComponent<AudioSource>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<newPlayer>();
        navAi = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Get the game manager instance
        var gameManagerObject = GameObject.Find("/GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        // Get the item assets to copy.
        ammoPack = GameObject.Find("/PotentialPowerUps/AmmoBox");
        health = GameObject.Find("/PotentialPowerUps/Health");
        unlimitedAmmo = GameObject.Find("/PotentialPowerUps/UnlimitedAmmo");

        // Get the weapons assets to copy.
        assualtRife = GameObject.Find("/WeaponObjects/AssualtRife");
        revolver = GameObject.Find("/WeaponObjects/Revolver");
        M1A1 = GameObject.Find("/WeaponObjects/M1A1");
        stenmk2 = GameObject.Find("/WeaponObjects/stenmk2");

    }

    // Update is called once per frame
    private void Update()
    {
        if (HP <= 0)
        {
            // Run on enemy death ->
            navAi.isStopped = true;

            if (!isDead)
            {
                // Decrement remaining enemies only once for each enemy.  
                gameManager.enemiesRemaining -= 1;
                isDead = true;
                spawnRandomItem();
            }

            return;
        }


        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= fitDistance)
        {
            navAi.isStopped = true;


            timer += Time.deltaTime;
            if (timer > time)
            {
                var rand = Random.Range(1, 3);
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
            player.TakeDamage(30);
        else
            player.TakeDamage(10);
    }

    public Slider hpslider;
    public bool isBoss = false;

    public void TakeDamage(float damage)
    {
        if (HP <= 0) return;


        HP -= damage;

        if (HP <= 0)
        {
            HP = 0;
            navAi.isStopped = true;
            animator.SetTrigger("die");
            hpslider.gameObject.SetActive(false);

            if (isBoss)
            {
                gameManager.playWinMusic();
                Invoke("toggleWinningScreen", 5);
            }

            Destroy(gameObject, 5);
        }

        hpslider.value = HP / maxhp;
    }

    public void toggleWinningScreen()
    {
        Time.timeScale = 0;

        var player = GameObject.Find("/Player");
        firstPersonController = player.GetComponent<FirstPersonController>();
        firstPersonController.unlockMouseLock();
        SceneManager.LoadScene("WinScene");
        Time.timeScale = 0;
    }

    // a function that will spawn a random item on enemy death.
    public void spawnRandomItem(){
        int randomNum = Random.Range(0, 100);
        if(randomNum <= 25){
            // 25% chance to drop ammo
            Instantiate(ammoPack, transform.position, ammoPack.transform.rotation);
        } else if(randomNum > 25 && randomNum <= 50){
            // 25% chance to drop health
            Instantiate(health, transform.position, ammoPack.transform.rotation);
        } else if(randomNum > 50 && randomNum <= 60){
            // 10% chance to drop unlimited ammo
            Instantiate(unlimitedAmmo, transform.position, ammoPack.transform.rotation);
        }else if(randomNum > 60 && randomNum <= 65){
            // 5% chance to drop a assualt rife
            Instantiate(assualtRife, transform.position, ammoPack.transform.rotation);
        }else if(randomNum > 65 && randomNum <= 75){
            // 10% chance to drop a revolver
            Instantiate(revolver, transform.position, ammoPack.transform.rotation);
        }else if(randomNum > 75 && randomNum <= 80){
            // 5% chance to drop a M1A1
            Instantiate(M1A1, transform.position, ammoPack.transform.rotation);
        }else if(randomNum > 80 && randomNum <= 85){
            // 10% chance to drop a stenmk2
            Instantiate(stenmk2, transform.position, ammoPack.transform.rotation);
        }else {
            // 15% chance to drop nothing.
        }
    }
}