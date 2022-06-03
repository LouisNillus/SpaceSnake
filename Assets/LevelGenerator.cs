using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public List<GameObject> chunks = new List<GameObject>();

    public Chunk lastChunk = null;


    public static LevelGenerator instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("SpawnChunk", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetChunk()
    {

    }

    public void SpawnChunk()
    {
        int i = Random.Range(0, chunks.Count);

        GameObject go = chunks[i];

        chunks.Remove(go);

        if(lastChunk == null)
        {
            //GameObject go = Instantiate(chunk1, Vector3.zero, Quaternion.identity);

            go.transform.position = Vector3.zero;

            lastChunk = go.GetComponent<Chunk>();
            lastChunk.ingame = true;
        }
        else
        {
            go.transform.position = lastChunk.connectionPoint + lastChunk.transform.position;

            lastChunk = go.GetComponent<Chunk>();
            lastChunk.ingame = true;
        }
    }
}
