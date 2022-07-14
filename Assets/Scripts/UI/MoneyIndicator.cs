using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyIndicator : MonoBehaviour
{

    public TextMeshProUGUI moneyText;

    int lastMoney = 0;

    private void OnEnable()
    {
        lastMoney = PlayerSave.instance.currentMoney;
        moneyText.text = lastMoney.ToString();
    }

    public IEnumerator RollToValue(float duration)
    {
        float t = 0f;

        int temp = lastMoney;
        lastMoney = PlayerSave.instance.currentMoney;
    
        while(t < duration)
        {
            moneyText.text = Mathf.Lerp(temp, PlayerSave.instance.currentMoney, t / duration).ToString("F0");
            t += Time.deltaTime;
            yield return null;
        }

    }

    public void UpdateIndicator()
    {
        if (this.gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(RollToValue(1f));
        }
    }
}
