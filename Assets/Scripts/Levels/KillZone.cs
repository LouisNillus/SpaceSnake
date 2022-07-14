using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{

    public Chunk chunk;


    public float killRadius = 0.5f;

    PlayerController player;


    private void Start()
    {
        player = PlayerController.instance;
    }

    private void Update()
    {
        if(chunk.ingame && Vector3.Distance(this.transform.position, PlayerController.instance.transform.position) < killRadius)
        {
            player.Kill();
            RunHandler.instance.EndRun();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, killRadius);
    }
}
