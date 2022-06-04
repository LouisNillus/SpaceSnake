using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform poolContainer;

    public List<GameObject> chunks = new List<GameObject>();

    public List<GameObject> chunksTemplates = new List<GameObject>();

    public Chunk lastChunk = null;

    public int maxDifficultyLevel;

    public Dictionary<int, List<Chunk>> chunksByDifficulty = new Dictionary<int, List<Chunk>>();


    public static LevelGenerator instance;

    


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialSpawn(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetChunk()
    {

    }

    public void SpawnChunk(int difficulty)
    {
        if (chunks.Count == 0) return;


        int i = Random.Range(0, chunks.Count);

        GameObject go = chunks[i];

        chunks.Remove(go);

        if(lastChunk == null)
        {
            go.transform.position = Vector3.zero;
        }
        else
        {
            go.transform.position = lastChunk.connectionPoint + lastChunk.transform.position;
        }

        lastChunk = go.GetComponent<Chunk>();
        lastChunk.ingame = true;
    }

    public void InitialSpawn(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            SpawnChunk(0);
        }
    }
}
