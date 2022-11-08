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
    [Header("Between Screen Transition")]
    public AnimationCurve pageSwitchAnimation;
    public float transitionTime;

    public RectTransform All;

    [Header("Main Page")]
    public RectTransform mainPage;
    public Button mainPlayBtn; //Uncomment if specific logic is needed
    public Button mainSettingsBtn;


    [Header("Settings")]
    public RectTransform settingsPage;
    public ToggleGroup settingsTG;
    public Toggle settingsDifficulty1;
    public Toggle settingsDifficulty2;
    public Toggle settingsDifficulty3;
    public Button settingsSaveButton;
    public Button settingsCancelButton;


    [Header("Game Over")]
    public RectTransform gameOverPage;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI gameOverMaxScore;
    public TextMeshProUGUI gameOverAttempt;
    public Button gameOverMenuButton;
    public Button gameOverRetryButton;

    private void Start()
    {
        if(PlayerPrefs.GetInt("JustDied",0)==1) //IF PLAYER HAS JUST DIE SHOW THEM GAME OVER SCREEN AND LOAD ITS VALUES; 
        {
            All.anchoredPosition = -gameOverPage.anchoredPosition;
            gameOverScore.   text = PlayerPrefs.GetInt("Score").ToString();
            gameOverMaxScore.text = PlayerPrefs.GetInt("MaxScore").ToString();
            gameOverAttempt. text = PlayerPrefs.GetInt("TotalAttempts").ToString();
            PlayerPrefs.SetInt("JustDied", 0);
        }
        
        switch (PlayerPrefs.GetInt("diff")) //Make settings in UI look according to real settings
        {
            case 1: settingsDifficulty1.isOn = true; break;
            case 2: settingsDifficulty2.isOn = true; break;
            case 3: settingsDifficulty3.isOn = true; break;
        }

        mainPlayBtn.onClick.AddListener(() =>{ SceneManager.LoadScene("MainGame") ;});
        mainSettingsBtn.onClick.AddListener(() =>{ StartCoroutine(MenuTransition(settingsPage.anchoredPosition)); });

        settingsSaveButton.onClick.AddListener(() => 
        { 
            PlayerPrefs.SetInt("diff", int.Parse(settingsTG.GetFirstActiveToggle().name)); 
            StartCoroutine(MenuTransition(mainPage.anchoredPosition));
        });
        settingsCancelButton.onClick.AddListener(() =>{StartCoroutine(MenuTransition(mainPage.anchoredPosition));});

        gameOverMenuButton.onClick.AddListener(() =>{StartCoroutine(MenuTransition(mainPage.anchoredPosition));});
        gameOverRetryButton.onClick.AddListener(() =>{ SceneManager.LoadScene("MainGame"); });

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
