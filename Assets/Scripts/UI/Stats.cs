using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{

    public TextMeshProUGUI scoreStat;
    public TextMeshProUGUI levelStat;
    public TextMeshProUGUI distanceStat;

    private void OnEnable()
    {
        WriteStats();
    }

    public void WriteStats()
    {
        scoreStat.text = PlayerSave.instance.bestScore.ToString();
        levelStat.text = PlayerSave.instance.bestLevel.ToString();
        distanceStat.text = PlayerSave.instance.bestDistance.ToString() +"m";
    }
}
