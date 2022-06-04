using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public Transform collectibleSpawnPoint;
    [HideInInspector] public Collectible collectible;

    public void SpawnCollectible(CollectibleType type = CollectibleType.Score)
    {
        GameObject go = CollectiblesManager.instance.FindCollectibleOfType(type);

        if (go == null) return; //Not enough collectibles in the pool

        collectible = go.GetComponent<Collectible>();
        collectible.transform.position = collectibleSpawnPoint.position;
        go.transform.parent = this.transform;
        
    }

    public void RecycleCollectible()
    {
        if (collectible == null) return;

        CollectiblesManager.instance.collectibles.Add(collectible.gameObject);
        collectible.gameObject.transform.parent = CollectiblesManager.instance.transform;
        collectible.gameObject.transform.position = CollectiblesManager.instance.transform.position;
        collectible = null;
    }
}
