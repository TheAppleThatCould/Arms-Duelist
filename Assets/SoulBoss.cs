using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SoulBoss : MonoBehaviour
{

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
    void Start()
    {
        maxhp = HP;
        time = timer;
        source = GetComponent<AudioSource>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<newPlayer>();
        navAi = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (HP <= 0) { navAi.isStopped = true; return; }



        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= fitDistance)//����
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
       
                Invoke("yanshiying",3); 
            }

            Destroy(this.gameObject,5);
        }

        hpslider.value = HP / maxhp;
    }

    public void yanshiying()
    {
        Time.timeScale = 0;
        GameManager.instence.Winpanel.SetActive(true);
    }
}
