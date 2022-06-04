using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DifficultyChunkList
{
    public int difficulty;
    public List<Chunk> chunks = new List<Chunk>();

    public DifficultyChunkList(int difficulty)
    {
        this.difficulty = difficulty;
    }
}
