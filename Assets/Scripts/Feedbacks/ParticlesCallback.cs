using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesCallback : MonoBehaviour
{

    private void OnParticleSystemStopped()
    {
        this.transform.position = CollectiblesManager.instance.transform.position;
        CollectiblesManager.instance.particlesPop.Add(this.GetComponent<ParticleSystem>());        
    }
}
