using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour, IDamageable
{
    public float health = 100f;
    public Text enemyHealth;
    public Player player;

    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            Destroy(gameObject);
        }
        Debug.Log("Health of enemy: " + health);
        enemyHealth.text = "Health: " + health.ToString();

        player.takeDamage(50);
    }
}
