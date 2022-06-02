using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEditor;
using System;

[CustomEditor(typeof(ChunkMaker))]
public class ChunkMakerInspector : Editor
{

    ChunkMaker chunkMaker;

    SerializedProperty chunkSize;
    SerializedProperty tileTemplate;
    SerializedProperty chunkTemplate;
    SerializedProperty tiles;
    SerializedProperty difficulty;

    public TileType typeOfTiles;

    string chunkName;

    private void OnEnable()
    {
        chunkMaker = (ChunkMaker)target;

        chunkSize = serializedObject.FindProperty("chunkSize");
        tileTemplate = serializedObject.FindProperty("tileTemplate");
        chunkTemplate = serializedObject.FindProperty("chunkTemplate");

        tiles = serializedObject.FindProperty("tiles");
        difficulty = serializedObject.FindProperty("difficulty");


        GetRandomChunkName();
    }
    

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(chunkSize);
        EditorGUILayout.PropertyField(tileTemplate);
        EditorGUILayout.PropertyField(chunkTemplate);

        typeOfTiles = (TileType)EditorGUILayout.EnumPopup("Tiles Type", typeOfTiles);

        if(tiles.arraySize == 0)
        {
            tiles.arraySize = chunkSize.vector2IntValue.x * chunkSize.vector2IntValue.y;
        }

        if (typeOfTiles == TileType.Floor)
        {
            GUILayout.BeginHorizontal();

            for (int i = 0; i < chunkSize.vector2IntValue.x; i++)
            {
                GUILayout.BeginVertical();

                for (int j = chunkSize.vector2IntValue.y - 1; j >= 0; j--)
                {
                    if (Get1DElementIndex(i, j).objectReferenceValue as GameObject != null) GUI.backgroundColor = Color.blue;
                    else GUI.backgroundColor = Color.white;

                    if (GUILayout.Button(""))
                    {
                        if (Get1DElementIndex(i, j).objectReferenceValue == null)
                        {
                            Get1DElementIndex(i, j).objectReferenceValue = Instantiate(tileTemplate.objectReferenceValue as GameObject, new Vector3(i, 0, j), Quaternion.identity);
                        }
                        else
                        {
                            DestroyImmediate(Get1DElementIndex(i, j).objectReferenceValue);
                        }

                    }


                }

                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
        }
        else if (typeOfTiles == TileType.Trap)
        {
            GUILayout.BeginHorizontal();

            for (int i = 0; i < chunkSize.vector2IntValue.x; i++)
            {
                GUILayout.BeginVertical();

                for (int j = chunkSize.vector2IntValue.y - 1; j >= 0; j--)
                {
                    if (Get1DElementIndex(i, j).objectReferenceValue as GameObject != null) GUI.backgroundColor = Color.blue;
                    else GUI.backgroundColor = Color.white;

                    if (GUILayout.Button(""))
                    {
                        if (Get1DElementIndex(i, j).objectReferenceValue == null)
                        {
                            Get1DElementIndex(i, j).objectReferenceValue = Instantiate(tileTemplate.objectReferenceValue as GameObject, new Vector3(i, 0, j), Quaternion.identity);
                        }
                        else
                        {
                            DestroyImmediate(Get1DElementIndex(i, j).objectReferenceValue);
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
            chunkMaker.tiles.CopyTo(chunk.tiles,0);

            for (int i = 0; i < tiles.arraySize; i++)
            {
                if (tiles.GetArrayElementAtIndex(i).objectReferenceValue != null)
                {
                    (tiles.GetArrayElementAtIndex(i).objectReferenceValue as GameObject).transform.position += go.transform.position;
                    (tiles.GetArrayElementAtIndex(i).objectReferenceValue as GameObject).transform.parent = go.transform;
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


    public SerializedProperty Get1DElementIndex(int x, int y)
    {
        return tiles.GetArrayElementAtIndex(x + (y * chunkSize.vector2IntValue.x));
    }
}

public enum TileType {Floor, Trap}
public enum TrapDirection { Floor, Trap }
