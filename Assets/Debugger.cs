using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debugger : MonoBehaviour
{

    public TextMeshProUGUI fpsCounter;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter.text = "FPS : " + (1f / Time.deltaTime).ToString("F0");
    }
}
