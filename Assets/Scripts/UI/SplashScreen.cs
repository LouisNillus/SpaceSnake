using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public void EnableGame()
    {
        RunHandler.instance.canStart = true;
        UIManager.instance.menuPanel.SetActive(true);


        this.gameObject.SetActive(false);
    }
}
