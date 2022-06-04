using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(ChunkMaker))]
public class ChunkMakerInspector : Editor
{

    ChunkMaker chunkMaker;

    SerializedProperty chunkSize;
    SerializedProperty tileTemplate;
    SerializedProperty trapTemplate;
    SerializedProperty killZoneTemplate;
    SerializedProperty chunkTemplate;
    SerializedProperty tiles;
    SerializedProperty traps;
    SerializedProperty difficulty;

    public TileType typeOfTiles;

    
    public TrapDirection trapDirection;
    public int activationDistance = 6;
    public int tilesTraveled = 1;

    string chunkName;

    private void OnEnable()
    {
        chunkMaker = (ChunkMaker)target;

        chunkSize = serializedObject.FindProperty("chunkSize");
        tileTemplate = serializedObject.FindProperty("tileTemplate");
        trapTemplate = serializedObject.FindProperty("trapTemplate");
        killZoneTemplate = serializedObject.FindProperty("killZoneTemplate");
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
        EditorGUILayout.PropertyField(killZoneTemplate);
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

        if (typeOfTiles == TileType.Floor) //Floor Map
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
                            Object o = Get1DElementIndex(tiles, i, j).objectReferenceValue = PrefabUtility.InstantiatePrefab(tileTemplate.objectReferenceValue);
                            GameObject go = o as GameObject;

                            go.transform.position = new Vector3(i, 0, j);
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

            if(trapDirection != TrapDirection.Static) tilesTraveled = EditorGUILayout.IntSlider("Tiles Traveled", tilesTraveled, 1, 9);

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
                            GameObject _trapTemplate = trapTemplate.objectReferenceValue as GameObject;
                            GameObject _tileTemplate = tileTemplate.objectReferenceValue as GameObject;



                            float posFromScale = (_trapTemplate.transform.localScale.y / 2f);

                            Object o = PrefabUtility.InstantiatePrefab(trapTemplate.objectReferenceValue);
                            GameObject go = o as GameObject;

                            go.transform.position = new Vector3(i, trapDirection == TrapDirection.Up ? -posFromScale : posFromScale + _tileTemplate.transform.localScale.y / 2f, j);


                            Trap trapComponent = go.GetComponent<Trap>();
                            trapComponent.direction = trapDirection;
                            trapComponent.activationDistance = activationDistance;
                            trapComponent.tilesMove = tilesTraveled;


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

            int lastLineIndex = LastTileLineIndex();

            chunk.allTiles = new GameObject[chunkSize.vector2IntValue.x * chunkSize.vector2IntValue.y];
            chunk.connectionPoint = go.transform.position.OffsetZ(lastLineIndex + 1) - (Vector3.right * chunkSize.vector2IntValue.x / 2);

            chunk.difficulty = difficulty.intValue;

            chunkMaker.tiles.CopyTo(chunk.allTiles,0);


            for (int i = 0; i < tiles.arraySize; i++)
            {
                Object tile = tiles.GetArrayElementAtIndex(i).objectReferenceValue;
                Object trap = traps.GetArrayElementAtIndex(i).objectReferenceValue;

                if (tile != null) //Tiles
                {
                    GameObject tileGO = tile as GameObject;

                    tileGO.transform.position += go.transform.position;
                    tileGO.transform.parent = go.transform;
                }
                else if (tile == null && Get2DPosFrom1D(i).y <= lastLineIndex) //Kill Zones
                {
                    Object o = PrefabUtility.InstantiatePrefab(killZoneTemplate.objectReferenceValue);
                    GameObject killZone = o as GameObject;

                    killZone.GetComponent<KillZone>().chunk = chunk;
                    killZone.transform.position = new Vector3(Get2DPosFrom1D(i).x, 0 + ((tileTemplate.objectReferenceValue as GameObject).transform.localScale.y / 2f), Get2DPosFrom1D(i).y);

                    killZone.transform.position += go.transform.position;
                    killZone.transform.parent = go.transform;
                }

                if(trap != null) //Traps
                {
                    GameObject trapGO = trap as GameObject;

                    trapGO.transform.position += go.transform.position;
                    trapGO.transform.parent = go.transform;
                    trapGO.GetComponent<Trap>().chunk = chunk;

                    chunk.traps.Add(trapGO.GetComponent<Trap>());
                }
            }

            chunk.filledTiles = chunk.allTiles.ToList<GameObject>();
            chunk.filledTiles.RemoveAll(x => x == null);

            go.name = chunkName + "D" + chunk.difficulty;
            PrefabUtility.SaveAsPrefabAsset(go, "Assets/Prefabs/Chunks/" + "D" + chunk.difficulty + "_" + chunkName +  ".prefab");

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

    public Vector2Int Get2DPosFrom1D(int index)
    {
        return new Vector2Int(index % chunkSize.vector2IntValue.x, index / chunkSize.vector2IntValue.x);
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
