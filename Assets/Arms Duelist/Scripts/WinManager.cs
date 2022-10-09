using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static int scores;
    private string name;
    private Scene scene;
    
    public Text scoreText;

    void Start(){
        Scene scene = SceneManager.GetActiveScene();
        name = scene.name;
    }

    void Update(){
        // Get the score from the gameManger durring the game and set the totalScore text if the player wins
        if(name == "Game"){
            GameObject gameManagerScript = GameObject.Find("/GameManager");
            gameManager = gameManagerScript.GetComponent<GameManager>();
            scores = gameManager.getScore();
        } else if (name == "WinScene"){
            scoreText.text = "Total Score: " + scores.ToString();
        }
    }

    public void LoadLevel (string levelName) {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName);
    }
}
