using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

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

    public bool canStart = false;

    private void Awake()
    {
        instance = this;
    }

    float skyboxRotation;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        maxDifficulty = LevelGenerator.instance.maxDifficultyLevel;

        WriteDifficulty();

        initialPlayerZ = player.transform.position.z;

        //PUT SOMEWHERE ELSE
        DOTween.To(() => skyboxRotation, x => skyboxRotation = x, 360, 360).SetLoops(-1, LoopType.Yoyo).OnUpdate(()=> RenderSettings.skybox.SetFloat("_Rotation", skyboxRotation));
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs(player.transform.position.z - initialPlayerZ);
        WriteDistance();        
    }

    public void Reset()
    {
        score = 0;
        scoreText.text = score.ToString();
        money = 0;
        distance = 0f;       
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
}
