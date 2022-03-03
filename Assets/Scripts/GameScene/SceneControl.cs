using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneControl : MonoBehaviour
{
    bool exitChk = false;
    bool exitPanelonOff = false;
    public GameObject exitPanel;
    public GameObject exitChkPanel;
    public Image deadPanel;
    public GameObject daedPanelObj;
    public GameObject diePanel;
    public Image startPanel;
    public GameObject startPanelObj;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        float fadeCount = 1;
        while (fadeCount > 0f)
        {
            fadeCount -= 0.005f;
            yield return new WaitForSeconds(0.005f);
            startPanel.color = new Color(0, 0, 0, fadeCount);
            if(fadeCount <=0.01f)
            {
                startPanelObj.SetActive(false);
            }
        }
    }
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

    public void BXButton()
    {
        exitPanelonOff = !exitPanelonOff;
        if (exitPanelonOff)
        {
            exitPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            exitPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void ExitChk()
    {
        exitChk = !exitChk;
        if (exitChk)
        {
            exitChkPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            exitChkPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void DeadPanel()
    {
        daedPanelObj.SetActive(true);
        StartCoroutine(DeadCoroutine());
    }

    IEnumerator DeadCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            deadPanel.color = new Color(0, 0, 0, fadeCount);

            if(fadeCount >=0.9f)
            {
                fadeCount = 1f;
                diePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

}
