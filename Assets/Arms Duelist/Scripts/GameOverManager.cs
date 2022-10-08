using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}