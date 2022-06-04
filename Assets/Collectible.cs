using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CollectiblesManager.instance.PopAtPos(this.transform.position);

            switch (type)
            {
                case CollectibleType.Score:
                    RunHandler.instance.AddScore();
                    break;
                case CollectibleType.Money:
                    RunHandler.instance.AddMoney();
                    break;
            }

            this.transform.position = CollectiblesManager.instance.transform.position;
        }
    }
}

public enum CollectibleType {Score, Money}
