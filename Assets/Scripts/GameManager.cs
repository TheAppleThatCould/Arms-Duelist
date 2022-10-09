using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Create variable for GameObject in the hierarchy
    public static GameManager instence;
    public newPlayer player;

    public List<Transform> sptrans = new List<Transform>();
    public List<GameObject> enemyprelist = new List<GameObject>();
    public GameObject Boss;

    // UI text for player interface ->
    public Text scoreText;
    public static int scores = 0;
    public Text RemainingEnemiesText;
    public Text CurrentWaveText;
    public Text currentAmmoText;
    public Text bulletText;

    // Variable for the different panels
    public GameObject Winpanel;
    public GameObject overpanel;
    public GameObject IntroductionPanel;

    // Game audio
    public AudioSource backgroundMusic;
    public AudioClip winMusic;

    // The current amount of enemies remaining.
    public int enemiesRemaining = 0;
    // Current wave
    public int currentWave = 0;
    // Timer to spawn enemies.
    private float timer = 14f;

    private void Awake()
    {
        // Get the player object
        var playerObject = GameObject.Find("/Player");
        player = playerObject.GetComponent<newPlayer>();

        instence = this;

        // Disable the introPanel in 5 seconds
        Invoke("toggleIntroPanel", 10);

        // Spawn enemies before the updateGame() because the updateGame will check the amount of enemies spawned.
        Invoke("SpawnEnemy", 13);
    }

    private void Update()
    {
        // Update the UI Text every frame
        updateUIText();
        
        // Spawn enemy if the current wave is finish and the current wave isn't final wave
        if (enemiesRemaining == 0 && currentWave <= 6){
            // Spawn enemies in 5 seconds.
            timer -= Time.deltaTime;
            if(timer <= 0){
                SpawnEnemy();
            }
        }
    }

    // A function that will spawn the enemies
    public void SpawnEnemy()
    {
        // If the current wave is the final wave then spawn the boss
        if (currentWave >= 6)
        {
            enemiesRemaining += 1;
            Boss.SetActive(true);
            return;
        }

        // Spawn 10 enemies
        for (var i = 0; i < 10; i++)
        {
            var spindex = Random.Range(0, sptrans.Count);
            var preindex = Random.Range(0, enemyprelist.Count);

            Instantiate(enemyprelist[preindex], sptrans[spindex].position, Quaternion.identity);

            // Increment the amount of enemies remaining for each enemy spawn.
            enemiesRemaining += 1;
        }

        // Increment the current wave count.
        currentWave += 1;

        // Set timer for every 5 seconds.
        timer = 5f;
    }

    // Add a single score to the player per enemy killed
    public void addScores()
    {
        scores += 1;
        scoreText.text = "score:" + scores.ToString();
    }

    // Update the player UI
    public void updateUIText()
    {
        RemainingEnemiesText.text = "Remaining Enemies: " + enemiesRemaining.ToString();
        CurrentWaveText.text = "Current Wave: " + currentWave.ToString();
        currentAmmoText.text = "Ammo: " + player.ammo.ToString();
        bulletText.text = "Bullet:" + player.BulletNum.ToString() + "/" + player.totalBulletNum.ToString();
    }

    // Display the introduction panel
    private void toggleIntroPanel()
    {
        IntroductionPanel.SetActive(false);
    }

    // Play the win music
    public void playWinMusic()
    {
        backgroundMusic.PlayOneShot(winMusic);
    }
}