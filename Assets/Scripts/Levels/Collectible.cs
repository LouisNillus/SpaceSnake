using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleType type;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CollectiblesManager.instance.PopAtPos(this.transform.position);

            Vibrator.Vibrate(175);

            switch (type)
            {
                case CollectibleType.Score:
                    RunHandler.instance.AddScore(1+RunHandler.instance.currentDifficulty);
                    UIManager.instance.rhombusLogo.Play();
                    break;
            }

            this.transform.position = CollectiblesManager.instance.transform.position;
        }
    }
}

public enum CollectibleType {Score, Money}
