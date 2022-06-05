using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using UnityEditor;
using System;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorInspector : Editor
{
    LevelGenerator levelGenerator;

    SerializedProperty poolContainer;
    SerializedProperty chunksTemplates;
    SerializedProperty activeChunks;
    SerializedProperty chunks;
    SerializedProperty maxDifficultyLevel;
    SerializedProperty initialChunksCount;

    int lastChunksCount = 0;

    bool foldoutStatus = true;

    private void OnEnable()
    {
        levelGenerator = (LevelGenerator)target;

        poolContainer = serializedObject.FindProperty("poolContainer");
        chunksTemplates = serializedObject.FindProperty("chunksTemplates");
        chunks = serializedObject.FindProperty("chunks");
        maxDifficultyLevel = serializedObject.FindProperty("maxDifficultyLevel");
        initialChunksCount = serializedObject.FindProperty("initialChunksCount");
        activeChunks = serializedObject.FindProperty("activeChunks");

        lastChunksCount = chunks.arraySize;

        ClearChunksArray();        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(poolContainer);
        EditorGUILayout.PropertyField(chunks);
        EditorGUILayout.PropertyField(activeChunks);
        EditorGUILayout.PropertyField(chunksTemplates);
        EditorGUILayout.PropertyField(maxDifficultyLevel);
        EditorGUILayout.PropertyField(initialChunksCount);

        EditorGUILayout.Space(20);

        foldoutStatus = EditorGUILayout.Foldout(foldoutStatus, "Chunks Panel");

        if (foldoutStatus)
        {
            foreach(GameObject go in levelGenerator.chunksTemplates)
            {
                GUILayout.BeginHorizontal();

                EditorGUILayout.ObjectField(go, typeof(GameObject), false);

                GUILayout.Label(GetCount(go).ToString()); ;

                if(GUILayout.Button("+"))
                {

                    Object o = PrefabUtility.InstantiatePrefab(go);
                    GameObject chunk = o as GameObject;
                    chunk.transform.parent = (poolContainer.objectReferenceValue as Transform);
                    chunk.transform.localPosition = Vector3.zero;

                    levelGenerator.sorted = false;

                    levelGenerator.chunks.Add(chunk);
                }

                if (GUILayout.Button("-"))
                {
                    GameObject chunk = FindFromPrefab(go);

                    levelGenerator.sorted = false;

                    levelGenerator.chunks.Remove(chunk);
                    DestroyImmediate(chunk);
                }

                GUILayout.EndHorizontal();
            }
        }

        if(lastChunksCount != chunks.arraySize)
        {
            levelGenerator.sorted = false;
            lastChunksCount = chunks.arraySize;
        }

        EditorGUILayout.HelpBox(!levelGenerator.sorted ? "Sort Required" : "Sort Complete", !levelGenerator.sorted ? MessageType.Warning : MessageType.Info);

        if (GUILayout.Button("Sort By Difficulty"))
        {

            levelGenerator.chunksByDifficulty.Clear();

            for (int i = 0;  i < maxDifficultyLevel.intValue;  i++)
            {
                levelGenerator.chunksByDifficulty.Add(new DifficultyChunkList(i));
            }

            for (int j = 0; j < chunks.arraySize; j++)
            {
                GameObject go = chunks.GetArrayElementAtIndex(j).objectReferenceValue as GameObject;

                if (go == null)
                {
                    chunks.DeleteArrayElementAtIndex(j);
                    j--;
                    continue;
                }

                Chunk chunk = go.GetComponent<Chunk>();

                List<Chunk> refChunks = levelGenerator.chunksByDifficulty[chunk.difficulty].chunks;

                refChunks.Add(chunk);
            }

            levelGenerator.sorted = true;

        serializedObject.ApplyModifiedProperties();
        }


        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(levelGenerator);
    }

    public int GetCount(GameObject go)
    {
        int result = 0;

        for (int i = 0; i < levelGenerator.chunks.Count; i++)
        {
            if(levelGenerator.chunks[i].name.Contains(go.name))
            {
                result++;
            }
        }

        return result;
    }

    public GameObject FindFromPrefab(GameObject go)
    {      
        for (int i = 0; i < levelGenerator.chunks.Count; i++)
        {
            if (levelGenerator.chunks[i].name.Contains(go.name))
            {
                return levelGenerator.chunks[i];
            }
        }

        return null;
    }

    public void ClearChunksArray()
    {
        for (int i = 0; i< chunks.arraySize; i++)
        {
            GameObject go = chunks.GetArrayElementAtIndex(i).objectReferenceValue as GameObject;

            if (go == null)
            {
                chunks.DeleteArrayElementAtIndex(i);
                i--;
                continue;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
