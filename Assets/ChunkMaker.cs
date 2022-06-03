using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMaker : MonoBehaviour
{
    public GameObject tileTemplate;
    public GameObject trapTemplate;
    public GameObject chunkTemplate;

    public int difficulty;
    public Vector2Int chunkSize;

    public GameObject[] tiles = new GameObject[0];
    public GameObject[] traps = new GameObject[0];
    public GameObject chunkContainer = null;
}
