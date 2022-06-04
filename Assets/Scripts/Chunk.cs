using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public int difficulty;

    [HideInInspector]
    public GameObject[] tiles = new GameObject[0];

    public Vector3 connectionPoint;

    public List<Trap> traps = new List<Trap>();

    [HideInInspector]
    public bool ingame;

    Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        if (ingame && this.transform.position.z + connectionPoint.z < cam.transform.position.z) Recycle();
    }

    public void Recycle()
    {
        this.transform.position = Vector3.one * Random.Range(-5000, -50);
        LevelGenerator.instance.chunks.Add(this.gameObject);
        ingame = false;

        foreach(Trap trap in traps)
        {
            trap.Reset();
        }

        LevelGenerator.instance.SpawnChunk(0);
    }

}
