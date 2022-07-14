using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public GameObject lostPanel;
    public GameObject ad;
    public GameObject closeAd;
    public GameObject tutoBar;
    public GameObject menuPanel;
    public GameObject splashScreen;
    public GameObject eventScreen;

    public Animator bottomBarAnimator;

    public UIScaler rhombusLogo;

    [HideInInspector] public ShopTheme activeTheme;

    private void Awake()
    {
        instance = this;

        if(!splashScreen.activeInHierarchy) //Debug sans splashscreen
        {
            RunHandler.instance.canStart = true;
            menuPanel.SetActive(true);
        }
    }

    public void LostPanel()
    {
        lostPanel.SetActive(true);
    }

    public void AdRoulette()
    {
        int i = Random.Range(0, 100);

        if (i > 50) ShowAd();
    }

    public void ShowAd()
    {
        ad.SetActive(true);
        StartCoroutine(ShowAdRoutine(5));
    }

    public IEnumerator ShowAdRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        closeAd.SetActive(true);
    }

    public void CloseBottomBar()
    {
        bottomBarAnimator.SetTrigger("BottomBarOut");
    }

    public void OpenBottomBar()
    {
        bottomBarAnimator.SetTrigger("BottomBarIn");
    }


}
