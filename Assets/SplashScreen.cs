using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public void EnableGame()
    {
        RunHandler.instance.canStart = true;
        this.gameObject.SetActive(false);
    }
}
