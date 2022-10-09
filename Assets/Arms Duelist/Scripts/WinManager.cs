using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public void LoadLevel (string levelName) {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName);
    }
}
