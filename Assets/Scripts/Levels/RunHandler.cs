using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class RunHandler : MonoBehaviour
{

    public static RunHandler instance;

    int score;
    [SerializeField] TextMeshProUGUI scoreText;
    float distance;
    [SerializeField] TextMeshProUGUI distanceText;

    int maxDifficulty;
    public int currentDifficulty = 0;
    [SerializeField] TextMeshProUGUI difficultyText;
    [SerializeField] Gradient difficultyGradient;

    float initialPlayerZ;

    PlayerController player;

    public float zSpeedModifier = 1f;

    public bool autoStart;

    [Range(0,100)]
    public int firstStep = 20;
    [Range(0,100)]
    public int nextSteps = 30;

    int remainingScore;

    [HideInInspector] public bool canStart = false;
    bool runStarted;

    public float minSpawnProbability;
    public float maxSpawnProbability;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = PlayerController.instance;
        maxDifficulty = LevelGenerator.instance.maxDifficultyLevel;

        WriteDifficulty();

        remainingScore = firstStep;
        initialPlayerZ = player.transform.position.z;
    }

    void Update()
    {
        distance = Mathf.Abs(player.transform.position.z - initialPlayerZ);
        WriteDistance();
        
        if((canStart && Input.touchCount > 0 && !Methods.IsPointerOverUIObject()) || autoStart)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began && !runStarted)
            {
                UIManager.instance.CloseBottomBar();
                player.StartPlayer();                
                UIManager.instance.tutoBar.SetActive(false);
                runStarted = true;
            }
        }
    }

    public void ResetRunInfos()
    {
        score = 0;
        scoreText.text = score.ToString();
        currentDifficulty = 0;
        WriteDifficulty();
        distance = 0f;
        WriteDistance();
    }

    public void AddScore(int value = 1)
    {
        score += value;
        remainingScore -= value;
        scoreText.text = score.ToString();
        CheckForEvent();
    }

    public void CheckForEvent()
    {
        if(remainingScore <= 0)
        {
            player.StopPlayer();
            SpawnEvent();

            remainingScore = nextSteps + (10 * currentDifficulty);
        }
    }

    public void IncreaseDifficulty(int value = 1)
    {
        currentDifficulty += value;
        WriteDifficulty();
    }

    public void IncreaseMinDropRate(float min)
    {
        minSpawnProbability += min;
    }
    
    public void IncreaseMaxDropRate(float max)
    {
        maxSpawnProbability += max;
    }

    public void IncreaseZSpeedMultiplier(float value)
    {
        zSpeedModifier += value;
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

    public void EndRun()
    {
        if(runStarted)
        {
            PlayerSave.instance.AddMoney(score);
            PlayerSave.instance.UpdateRecords(score, currentDifficulty, (int)distance);
            canStart = false;
            runStarted = false;
        }
    }

    public void NewRun()
    {
        player.ResetPlayer();
        LevelGenerator.instance.ResetActiveChunks();

        zSpeedModifier = 1f;
        remainingScore = firstStep;
        minSpawnProbability = 0f;
        maxSpawnProbability = 1f;
        LevelGenerator.instance.InitialSpawn();
        UIManager.instance.tutoBar.SetActive(true);
        UIManager.instance.OpenBottomBar();
        ResetRunInfos();

        canStart = true;
    }


    public void SpawnEvent()
    {
        UIManager.instance.eventScreen.SetActive(true);
    }
}
