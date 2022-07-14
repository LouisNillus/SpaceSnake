using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{

    public static PlayerSave instance;

    public int bestScore;
    public int bestLevel;
    public int bestDistance;

    public int currentMoney;

    public string unlockedThemes;

    public int currentTheme;
    public int currentModel;


    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("unlockedThemes")) PlayerPrefs.SetString("unlockedThemes", "0000000");

        LoadData();
    }

    public void SaveData()
    {
        Customizer customizer = Customizer.instance;

        unlockedThemes = "";

        for (int i = 0; i < customizer.allThemes.Count; i++)
        {
            unlockedThemes += customizer.allThemes[i].bought ? '1' : '0';
        }

        PlayerPrefs.SetInt("bestScore", bestScore);
        PlayerPrefs.SetInt("bestLevel", bestLevel);
        PlayerPrefs.SetInt("bestDistance", bestDistance);
        PlayerPrefs.SetInt("currentMoney", currentMoney);
        PlayerPrefs.SetString("unlockedThemes", unlockedThemes);
        PlayerPrefs.SetInt("currentTheme", currentTheme);
        PlayerPrefs.SetInt("currentModel", currentModel);
    }

    public void LoadData()
    {
        bestScore = PlayerPrefs.GetInt("bestScore");
        bestLevel = PlayerPrefs.GetInt("bestLevel");
        bestDistance = PlayerPrefs.GetInt("bestDistance");
        currentMoney = PlayerPrefs.GetInt("currentMoney");
        unlockedThemes = PlayerPrefs.GetString("unlockedThemes");
        currentTheme = PlayerPrefs.GetInt("currentTheme");
        currentModel = PlayerPrefs.GetInt("currentModel");
    }

    public void AddMoney(int value)
    {
        currentMoney += value;
        SaveData();
    }

    public void UpdateRecords(int score, int level, int distance)
    {
        if (score > bestScore) bestScore = score;
        if (level > bestLevel) bestLevel = level;
        if (distance > bestDistance) bestDistance = distance;

        SaveData();
    }

    private void OnApplicationQuit()
    {
        //SaveData();
    }
}
