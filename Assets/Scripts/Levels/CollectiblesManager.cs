using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{

    public static CollectiblesManager instance;

    public List<GameObject> collectibles = new List<GameObject>();
    public List<ParticleSystem> particlesPop = new List<ParticleSystem>();

    public float probability;

    private void Awake()
    {
        instance = this;
    }
    public void PopAtPos(Vector3 position)
    {
        if(particlesPop.Count > 0)
        {
            ParticleSystem ps = particlesPop[0];
            ps.transform.position = position;
            ps.Play();

            particlesPop.Remove(ps);
        }
    }

    public GameObject FindCollectible()
    {
        if (collectibles.Count > 0)
        {
            GameObject go = collectibles[0];
            collectibles.Remove(go);
            return go;
        }
        else return null;
    }

    public GameObject FindCollectibleOfType(CollectibleType type)
    {
        if (collectibles.Count > 0)
        {
            for (int i = 0; i < collectibles.Count; i++)
            {
                GameObject go = collectibles[i];

                if (go.GetComponent<Collectible>().type == type)
                {
                    collectibles.Remove(go);
                    return go;
                }               
            }

        }

        return null;
    }
}
