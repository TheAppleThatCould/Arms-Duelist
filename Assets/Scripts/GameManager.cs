using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instence;
    public newPlayer player;

    public List<Transform> sptrans = new List<Transform>();
    public List<GameObject> enemyprelist = new List<GameObject>();

    public int boci = 5;

    public GameObject Boss;

    // UI text for player interface ->
    public Text NumText;
    public int NUm;
    public Text RemainingEnemiesText;
    public Text CurrentWaveText;
    public Text currentAmmoText;
    public Text bulletText;

    public GameObject Winpanel;
    public GameObject overpanel;
    public GameObject IntroductionPanel;


    // The current amount of enemies remaining.
    public int enemiesRemaining = 0;

    private int currentWave = 0;

    private void Awake()
    {
        GameObject playerObject = GameObject.Find("/Player");
        player = playerObject.GetComponent<newPlayer>();

        instence = this;

        // Disable the introPanel in 5 seconds
        Invoke("toggleIntroPanel", 10);

        // The built-in update() was too fast, therefore the InvokeRepeating is used to check the remaining enemies.
        InvokeRepeating("updateGame", 14, 3);

        // Spawn enemies before the updateGame() because the updateGame will check the amount of enemies spawned.
        Invoke("SpawnEnemy", 2);
    }

    void Update(){
        // Update the UI Text every frame
        updateUIText();
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

    public void updateUIText(){
        RemainingEnemiesText.text = "Remaining Enemies: " + enemiesRemaining.ToString();
        CurrentWaveText.text = "Current Wave: " + currentWave.ToString();
        currentAmmoText.text = "Ammo: " + player.ammo.ToString();
        bulletText.text = "Bullet:" + player.BulletNum.ToString() + "/" + player.totalBulletNum.ToString();
    }

    private void toggleIntroPanel(){
        IntroductionPanel.SetActive(false);
    }
}
