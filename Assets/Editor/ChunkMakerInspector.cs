using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using UnityEditor;
using System;

[CustomEditor(typeof(ChunkMaker))]
public class ChunkMakerInspector : Editor
{

    ChunkMaker chunkMaker;

    SerializedProperty chunkSize;
    SerializedProperty tileTemplate;
    SerializedProperty trapTemplate;
    SerializedProperty chunkTemplate;
    SerializedProperty tiles;
    SerializedProperty traps;
    SerializedProperty difficulty;

    public TileType typeOfTiles;

    [Header("Trap Settings")]
    public TrapDirection trapDirection;
    public int activationDistance;

    string chunkName;

    private void OnEnable()
    {
        chunkMaker = (ChunkMaker)target;

        chunkSize = serializedObject.FindProperty("chunkSize");
        tileTemplate = serializedObject.FindProperty("tileTemplate");
        trapTemplate = serializedObject.FindProperty("trapTemplate");
        chunkTemplate = serializedObject.FindProperty("chunkTemplate");

        tiles = serializedObject.FindProperty("tiles");
        traps = serializedObject.FindProperty("traps");
        difficulty = serializedObject.FindProperty("difficulty");


        GetRandomChunkName();
    }
    

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(chunkSize);
        EditorGUILayout.PropertyField(tileTemplate);
        EditorGUILayout.PropertyField(trapTemplate);
        EditorGUILayout.PropertyField(chunkTemplate);

        typeOfTiles = (TileType)EditorGUILayout.EnumPopup("Tiles Type", typeOfTiles);

        if(tiles.arraySize == 0)
        {
            tiles.arraySize = chunkSize.vector2IntValue.x * chunkSize.vector2IntValue.y;
        }

        if (traps.arraySize == 0)
        {
            traps.arraySize = chunkSize.vector2IntValue.x * chunkSize.vector2IntValue.y;
        }

        if (typeOfTiles == TileType.Floor)
        {
            GUILayout.BeginHorizontal();

            for (int i = 0; i < chunkSize.vector2IntValue.x; i++)
            {
                GUILayout.BeginVertical();

                for (int j = chunkSize.vector2IntValue.y - 1; j >= 0; j--)
                {
                    if (Get1DElementIndex(tiles, i, j).objectReferenceValue as GameObject != null) GUI.backgroundColor = Color.blue;
                    else GUI.backgroundColor = Color.white;

                    if (GUILayout.Button(""))
                    {
                        if (Get1DElementIndex(tiles, i, j).objectReferenceValue == null)
                        {
                            Get1DElementIndex(tiles, i, j).objectReferenceValue = Instantiate(tileTemplate.objectReferenceValue as GameObject, new Vector3(i, 0, j), Quaternion.identity);
                        }
                        else
                        {
                            DestroyImmediate(Get1DElementIndex(tiles, i, j).objectReferenceValue);
                        }

                    }


                }

                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
        }
        else if (typeOfTiles == TileType.Trap) //Trap Map
        {
            EditorGUILayout.Space(20);

            trapDirection = (TrapDirection)EditorGUILayout.EnumPopup("Direction", trapDirection);
            activationDistance = EditorGUILayout.IntSlider("Trigger Distance", activationDistance, 1, 20);

            GUILayout.BeginHorizontal();

            for (int i = 0; i < chunkSize.vector2IntValue.x; i++)
            {
                GUILayout.BeginVertical();

                for (int j = chunkSize.vector2IntValue.y - 1; j >= 0; j--)
                {
                    Object trap = Get1DElementIndex(traps, i, j).objectReferenceValue;
                    bool trapFound = (trap as GameObject != null);
                    string label = "";

                    if (trapFound)
                    {
                        GUI.backgroundColor = Color.red;
                        label += (trap as GameObject).GetComponent<Trap>().GetTrapCode();
                    }
                    else if(Get1DElementIndex(tiles, i, j).objectReferenceValue != null)
                    {
                        GUI.backgroundColor = Color.cyan;
                    }
                    else GUI.backgroundColor = Color.white;

                    if (GUILayout.Button(label))
                    {
                        if (!trapFound)
                        {
                            GameObject go = Instantiate(trapTemplate.objectReferenceValue as GameObject, new Vector3(i, trapDirection == TrapDirection.Up ? -1f : 0.35f, j), Quaternion.identity);


                            Trap trapComponent = go.GetComponent<Trap>();
                            trapComponent.direction = trapDirection;
                            trapComponent.activationDistance = activationDistance;


                            Get1DElementIndex(traps, i, j).objectReferenceValue = go;
                        }
                        else
                        {
                            DestroyImmediate(Get1DElementIndex(traps, i, j).objectReferenceValue);
                        }

                    }


                }

                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
        }

        GUI.backgroundColor = Color.white;

        EditorGUILayout.Space(20);

        chunkName = GUILayout.TextField(chunkName);
        EditorGUILayout.PropertyField(difficulty);

        EditorGUILayout.Space(10);

        if(GUILayout.Button("Save Chunk"))
        {
            GameObject go = Instantiate(chunkTemplate.objectReferenceValue as GameObject, (Vector3.right * chunkSize.vector2IntValue.x / 2), Quaternion.identity);

            Chunk chunk = go.GetComponent<Chunk>();

            chunk.tiles = new GameObject[chunkSize.vector2IntValue.x * chunkSize.vector2IntValue.y];
            chunk.connectionPoint = go.transform.position.OffsetZ(LastTileLineIndex() + 1) - (Vector3.right * chunkSize.vector2IntValue.x / 2);

            chunkMaker.tiles.CopyTo(chunk.tiles,0);

            for (int i = 0; i < tiles.arraySize; i++)
            {
                Object tile = tiles.GetArrayElementAtIndex(i).objectReferenceValue;
                Object trap = traps.GetArrayElementAtIndex(i).objectReferenceValue;

                if (tile != null)
                {
                    GameObject tileGO = tile as GameObject;

                    tileGO.transform.position += go.transform.position;
                    tileGO.transform.parent = go.transform;
                }

                if(trap != null)
                {
                    GameObject trapGO = trap as GameObject;

                    trapGO.transform.position += go.transform.position;
                    trapGO.transform.parent = go.transform;
                    trapGO.GetComponent<Trap>().chunk = chunk;

                    chunk.traps.Add(trapGO.GetComponent<Trap>());
                }
            }
           
            go.name = chunkName;
            PrefabUtility.SaveAsPrefabAsset(go, "Assets/Prefabs/Chunks/" + chunkName + ".prefab");

            GetRandomChunkName();

            DestroyImmediate(go);
        }

        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(chunkMaker);
    }

    public void GetRandomChunkName()
    {
        chunkName = "Chunk_" + Random.Range(0, 9999).ToString();
    }


    public SerializedProperty Get1DElementIndex(SerializedProperty from, int x, int y)
    {
        return from.GetArrayElementAtIndex(x + (y * chunkSize.vector2IntValue.x));
    }

    public int LastTileLineIndex()
    {
        int index = 0;

        for (int i = 0; i < tiles.arraySize; i++)
        {
            if (tiles.GetArrayElementAtIndex(i).objectReferenceValue != null) index = i;
        }

        return index / chunkSize.vector2IntValue.x;
    }
}

public enum TileType {Floor, Trap}
