using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopModel : MonoBehaviour
{
    public ValueToReach valueToReach;
    public int value;

    bool unlocked = false;

    public GameObject blocker;
    public Mesh mesh;

    public TextMeshProUGUI modelName;
    public TextMeshProUGUI goalText;



    void OnEnable()
    {
        WriteGoal();
        CheckForUnlock();
    }

    public void Equip()
    {
        if (!unlocked) return;

        Customizer customizer = Customizer.instance;

        if(customizer.currentModel != null)
        {
            customizer.currentModel.modelName.color = Color.white;
        }

        modelName.color = Color.yellow;

        PlayerSave.instance.currentModel = customizer.allModels.IndexOf(this);

        customizer.ChangeModel(mesh);
        customizer.currentModel = this;


        PlayerSave.instance.SaveData();
    }

    public void CheckForUnlock()
    {
        switch (valueToReach)
        {
            case ValueToReach.Score:
                if (PlayerSave.instance.bestScore >= value) Unlock();
                break;
            case ValueToReach.Level:
                if (PlayerSave.instance.bestLevel >= value) Unlock();
                break;
            case ValueToReach.Distance:
                if (PlayerSave.instance.bestDistance >= value) Unlock();
                break;
        }

    }

    public void Unlock()
    {
        blocker.SetActive(false);
        unlocked = true;
    }

    public void WriteGoal()
    {    
        switch (valueToReach)
        {
            case ValueToReach.Score:
                goalText.text = value.ToString();
                break;
            case ValueToReach.Level:
                goalText.text = "lvl " + value.ToString();
                break;
            case ValueToReach.Distance:
                goalText.text = value.ToString() +"m";
                break;
        }
    }
}

public enum ValueToReach {Score, Level, Distance}