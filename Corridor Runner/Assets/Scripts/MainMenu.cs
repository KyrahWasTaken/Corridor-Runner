using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Transform difficulty;
    public Transform gameOverScreen;

    public RectTransform All;
    public AnimationCurve pageSwitchAnimation;

    public Vector3 mainPosition;
    public Vector3 settingsPosition;
    public Vector3 GameOverPosition;

    public float transitionTime;
    private void Start()
    {
        if(PlayerPrefs.GetInt("JustDied",0)==1)
        {
            All.anchoredPosition = -GameOverPosition;
            gameOverScreen.Find("Score").Find("Value").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("Score").ToString();
            gameOverScreen.Find("MaxScore").Find("Value").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("MaxScore").ToString();
            gameOverScreen.Find("Attempt").Find("Value").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("TotalAttempts").ToString();
        }
        PlayerPrefs.SetInt("JustDied", 0);
        difficulty.Find(PlayerPrefs.GetInt("diff").ToString()).GetComponent<Toggle>().isOn = true;
    }
    public void SaveSettings()
    {
        if (PlayerPrefs.HasKey("diff"))
        PlayerPrefs.SetInt("diff", int.Parse(toggleGroup.GetFirstActiveToggle().name));
    }
    public void Settings()
    {
        StartCoroutine(MenuTransition(settingsPosition));
    }
    public void MainPage()
    {
        StartCoroutine(MenuTransition(mainPosition));
    }
    public void Play()
    {
        SceneManager.LoadScene("MainGame");
    }
    IEnumerator MenuTransition(Vector3 to)
    {
        Vector3 position = All.anchoredPosition;
        float timeStart = Time.time;
        float currentTime = timeStart;
        while (currentTime - timeStart < transitionTime)
        {
            All.anchoredPosition = Vector3.LerpUnclamped(position, -to, pageSwitchAnimation.Evaluate((currentTime-timeStart)/transitionTime));
            currentTime = Time.time;
            yield return new WaitForEndOfFrame();
        }
        All.anchoredPosition = -to;
    }
}
