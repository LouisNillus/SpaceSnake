using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunHandler : MonoBehaviour
{

    public static RunHandler instance;

    public int score;
    public TextMeshProUGUI scoreText;
    public int money;
    public float distance;
    public int currentDifficulty = 0;

    float initialPlayerZ;

    PlayerController player;

    public bool canStart = false;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerController.instance;
        initialPlayerZ = player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs(player.transform.position.z - initialPlayerZ);
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
}
