using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Chunk : MonoBehaviour
{
    public int difficulty;

    //[HideInInspector]
    public GameObject[] allTiles = new GameObject[0];
    public List<GameObject> filledTiles;

    public Vector3 connectionPoint;

    public List<Trap> traps = new List<Trap>();

    //[HideInInspector]
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

    public void Recycle(bool respawn = true)
    {
        this.transform.position = LevelGenerator.instance.poolContainer.position;
        LevelGenerator.instance.chunks.Add(this.gameObject);
        LevelGenerator.instance.activeChunks.Remove(this);

        LevelGenerator.instance.chunksByDifficulty[difficulty].chunks.Add(this);

        

        ingame = false;

        foreach(Trap trap in traps)
        {
            trap.Reset();
        }

        foreach(GameObject go in filledTiles)
        {
            go.GetComponent<Tile>().RecycleCollectible();
        }

        if(respawn) LevelGenerator.instance.SpawnChunk(RunHandler.instance.currentDifficulty);
    }

    public void PopRandomCollectibles(CollectibleType type)
    {
        float rand = Random.Range(RunHandler.instance.minSpawnProbability, RunHandler.instance.maxSpawnProbability);

        int index = Mathf.RoundToInt(rand);

        for (int i = 0; i < index; i++)
        {
            filledTiles[Random.Range(0, filledTiles.Count)].GetComponent<Tile>().SpawnCollectible(type);
        }
    }



}