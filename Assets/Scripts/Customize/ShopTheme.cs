using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTheme : MonoBehaviour
{

    public int price = 0;

    public bool bought;

    public ColorTheme theme;

    public TextMeshProUGUI priceText;

    public Image tileColorImage;
    public Image trapColorImage;
    public Image collectibleColorImage;

    public MoneyIndicator moneyIndicator;

    public TextMeshProUGUI themeName;



    void OnEnable()
    {
        PriceTextFeedback();
        VisualFeedBack();
        moneyIndicator.UpdateIndicator();
        priceText.transform.parent.gameObject.SetActive(!bought);
    }


    public bool CanBuy()
    {
        return price <= PlayerSave.instance.currentMoney;
    }

    public void Buy()
    {
        if(CanBuy())
        {
            bought = true;
            PlayerSave.instance.AddMoney(-price);
            moneyIndicator.UpdateIndicator();
            priceText.transform.parent.gameObject.SetActive(false);
        }
    }

    public void Action()
    {
        if (!bought) Buy();
        else Equip();
    }

    public void BoughtState(bool value) => bought = value;

    public void Equip()
    {
        Customizer customizer = Customizer.instance;
        ShopTheme activeTheme = UIManager.instance.activeTheme;


        if (activeTheme != null)
        {
            UIManager.instance.activeTheme.themeName.color = Color.white;
        }

        themeName.color = theme.tileColor;
        UIManager.instance.activeTheme = this;

        PlayerSave.instance.currentTheme = customizer.allThemes.IndexOf(this);
        customizer.ChangeTheme(theme);


        PlayerSave.instance.SaveData();
    }

    public void PriceTextFeedback()
    {
        priceText.text = price.ToString();
        priceText.color = CanBuy() ? Color.white : Color.red;
    }

    public void VisualFeedBack()
    {
        tileColorImage.color = theme.tileColor;
        trapColorImage.color = theme.trapColor;
        collectibleColorImage.color = theme.collectibleColor;
    }
}
