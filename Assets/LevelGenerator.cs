using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform poolContainer;

    public List<GameObject> chunks = new List<GameObject>();
    public List<Chunk> activeChunks = new List<Chunk>();

    public List<GameObject> chunksTemplates = new List<GameObject>();

    public Chunk lastChunk = null;

    public int maxDifficultyLevel;

    public List<DifficultyChunkList> chunksByDifficulty = new List<DifficultyChunkList>();


    public static LevelGenerator instance;

    public int initialChunksCount;

    public bool sorted;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ResetActiveChunks()
    {
        for (int i = 0; i < activeChunks.Count; i++)
        {
            activeChunks[i].Recycle(false);
            i--; //Car dans la fonction de Recycle l'objet se retire de la liste
        }

        lastChunk = null;
    }

    public void SpawnChunk(int difficulty)
    {
        if (chunks.Count == 0) return;

        List<Chunk> currentDifficultyChunks = chunksByDifficulty[difficulty].chunks;

        if (currentDifficultyChunks.Count == 0) return;


        Chunk c = currentDifficultyChunks[Random.Range(0, currentDifficultyChunks.Count)];

        c.PopRandomCollectibles(CollectibleType.Score, 3);

        currentDifficultyChunks.Remove(c);
        chunks.Remove(c.gameObject);
        activeChunks.Add(c);

        if (lastChunk == null)
        {
            c.transform.position = Vector3.zero;
        }
        else
        {
            c.transform.position = lastChunk.connectionPoint + lastChunk.transform.position;
        }

        lastChunk = c;
        lastChunk.ingame = true;
    }

    public void InitialSpawn()
    {
        for (int i = 0; i < initialChunksCount; i++)
        {
            SpawnChunk(0);
        }
    }


}
