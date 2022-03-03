using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlMain : MonoBehaviour
{
    public void GameStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
}
