using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class RunHandler : MonoBehaviour
{

    public static RunHandler instance;

    public int score;
    public TextMeshProUGUI scoreText;
    public int money;
    public float distance;
    public TextMeshProUGUI distanceText;

    int maxDifficulty;
    public int currentDifficulty = 0;
    public TextMeshProUGUI difficultyText;
    public Gradient difficultyGradient;

    float initialPlayerZ;

    PlayerController player;

    public bool autoStart;

    public bool canStart = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        maxDifficulty = LevelGenerator.instance.maxDifficultyLevel;

        WriteDifficulty();

        initialPlayerZ = player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs(player.transform.position.z - initialPlayerZ);
        WriteDistance();
        
        if((canStart && Input.touchCount > 0 && !Methods.IsPointerOverUIObject()) || autoStart)
        {            
            player.StartPlayer();
            LevelGenerator.instance.InitialSpawn();
            UIManager.instance.tutoBar.SetActive(false);      
        }
    }

    public void ResetRunInfos()
    {
        score = 0;
        scoreText.text = score.ToString();
        money = 0;
        currentDifficulty = 0;
        WriteDifficulty();
        distance = 0f;
        WriteDistance();
    }

    public void AddScore(int value = 1)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void AddMoney(int value = 1)
    {
        money += value;
    }

    public void IncreaseDifficulty(int value = 1)
    {
        currentDifficulty += value;
        WriteDifficulty();
    }

    public void WriteDifficulty()
    {
        difficultyText.text = "LEVEL " + currentDifficulty.ToString();
        difficultyText.color = difficultyGradient.Evaluate(Mathf.InverseLerp(0, maxDifficulty, currentDifficulty));
    }

    public void WriteDistance()
    {
        distanceText.text = distance.ToString("F0") + "m";
    }

    public void NewRun()
    {
        LevelGenerator.instance.ResetActiveChunks();
        UIManager.instance.tutoBar.SetActive(true);
        ResetRunInfos();
    }
}
