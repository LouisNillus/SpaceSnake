using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{

    public static CollectiblesManager instance;

    public List<ParticleSystem> particlesPop = new List<ParticleSystem>();

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
}
