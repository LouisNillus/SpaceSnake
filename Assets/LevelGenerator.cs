using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject chunk1;
    public GameObject chunk2;

    public Chunk currentChunk;

    public Vector3 connectionPoint { get; }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(chunk1, Vector3.zero, Quaternion.identity);
        Instantiate(chunk2, Vector3.forward * 5f, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetChunk()
    {

    }
}
