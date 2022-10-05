using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instence;

    public List<Transform> sptrans = new List<Transform>();
    public List<GameObject> enemyprelist = new List<GameObject>();

    public int boci = 5;

    public GameObject Boss;

    public Text NumText;
    public int NUm;

    public GameObject Winpanel;
    public GameObject overpanel;

    // The current amount of enemies remaining.
    public int enemiesRemaining = 0;

    private int currentWave = 0;

    private void Awake()
    {
        instence = this;

        // The built-in update() was too fast, therefore the InvokeRepeating is used to check the remaining enemies.
        InvokeRepeating("updateGame",2,3);

        // Spawn enemies straight away.
        Invoke("SpawnEnemy", 1);
    }

    private void updateGame(){
        // check if all the enemies are dead and limit the amount of waves
        if(enemiesRemaining == 0 && currentWave <= 6){
            Invoke("SpawnEnemy", 5);
        }
    }

    public void SpawnEnemy()
    {
        if (boci<1)
        {
            Boss.SetActive(true);
            return;
        }

        boci--;
        for (int i = 0; i < 10; i++)
        {
            
            int spindex = Random.Range(0, sptrans.Count);
            int preindex = Random.Range(0, enemyprelist.Count);

            Instantiate(enemyprelist[preindex], sptrans[spindex].position,Quaternion.identity);

            // Increment the amount of enemies remaining for each enemy spawn.
            enemiesRemaining += 1;
        }

        // Increment the current wave count.
        currentWave += 1;
    }

    public void AddNum()
    {
        NUm++;
        NumText.text = "score:"+NUm.ToString();
    }
}
