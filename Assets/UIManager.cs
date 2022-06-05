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

    public UIScaler rhombusLogo;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


}
