using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static int difficulty;
    public static int maxScore;

    public static void SaveData()
    {
        PlayerPrefs.SetInt("diff", difficulty);
        PlayerPrefs.SetInt("Score", maxScore);
    }
}
