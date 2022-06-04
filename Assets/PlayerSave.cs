using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{

    public static PlayerSave instance;

    public int bestScore;

    public float bestDistance;

    private void Awake()
    {
        instance = this;    
    }
}
