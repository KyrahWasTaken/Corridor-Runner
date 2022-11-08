using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float difficulty;
    public float time2SpeedIncrease;
    public float speedIncrease;
    public int score;

    public TextMeshProUGUI uiScore;
    private void Start()
    {
        difficulty = PlayerPrefs.GetInt("diff");
        StartCoroutine(IncreaseSpeed());
        StartCoroutine(IncreaseScore());
        uiScore.transform.parent.parent.Find("Speed").Find("Value").GetComponent<TextMeshProUGUI>().text = ((int)speed).ToString();
        uiScore.transform.parent.parent.Find("Diff").Find("Value").GetComponent<TextMeshProUGUI>().text = difficulty.ToString();
    }

    private IEnumerator IncreaseScore()
    {
        while(true)
        {
            score++;
            uiScore.text = score.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        while(true)
        {
            yield return new WaitForSeconds(time2SpeedIncrease);
            uiScore.transform.parent.parent.Find("Speed").Find("Value").GetComponent<TextMeshProUGUI>().text = ((int)speed).ToString();
            uiScore.transform.parent.parent.Find("Diff").Find("Value").GetComponent<TextMeshProUGUI>().text = difficulty.ToString();
            speed += speedIncrease * difficulty;
        }
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") > 0) 
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        else
            transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Lol Youre dead");
        PlayerPrefs.SetInt("TotalAttempts", PlayerPrefs.GetInt("TotalAttempts", 0) + 1);
        PlayerPrefs.SetInt("MaxScore", Mathf.Max(PlayerPrefs.GetInt("MaxScore", 0),score));
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("JustDied", 1);
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
}
