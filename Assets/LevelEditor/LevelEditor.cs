using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateLevel))]
[CanEditMultipleObjects]
public class LevelEditor : Editor
{
    GameObject wallPrefab;
    CreateLevel create;

    bool scriptActive;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        create = (CreateLevel)target;
        wallPrefab = create.wall;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Border"))
        {
            if (!scriptActive)
            {
                BuildBorder();
            }
        }
        if (GUILayout.Button("Delete Border"))
        {
            if (!scriptActive)
            {
                DeleteBorder();
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create innerwalls"))
        {
            if (!scriptActive)
            {
                BuildInnerWalls();
            }
        }
        if (GUILayout.Button("Delete innerwalls"))
        {
            if (!scriptActive)
            {
                DeleteInnerWalls();
            }
        }
        EditorGUILayout.EndHorizontal();
    }
    void BuildBorder()
    {
        if (create.gridSizeX < 5 || create.gridSizeZ < 5)
        {
            Debug.LogWarning("Grid size must be bigger or equal to five");
            return;
        }
        if (create.gridSizeX % 2 == 0 || create.gridSizeZ % 2 == 0)
        {
            Debug.LogWarning("Gridsize must be an uneven number");
            return;
        }
        DeleteBorder();
        scriptActive = true;
        for (int i = 0; i < create.gridSizeX; i++)
        {
            for (int j = 0; j < create.gridSizeZ; j++)
            {
                if (i == 0 || i == create.gridSizeX - 1)
                {
                    GameObject wall = PrefabUtility.InstantiatePrefab(wallPrefab) as GameObject;
                    wall.transform.position = new Vector3(create.start.x + i + create.offset.x,
                                                          create.start.y + create.offset.y,
                                                          create.start.z + j + create.offset.z);
                    wall.transform.parent = create.outerWallHolder;
                }

                if (j == 0 || j == create.gridSizeZ - 1)
                {
                    GameObject wall = PrefabUtility.InstantiatePrefab(wallPrefab) as GameObject;
                    wall.transform.position = new Vector3(create.start.x + i + create.offset.x,
                                                  create.start.y + create.offset.y,
                                                  create.start.z + j + create.offset.z);
                    wall.transform.parent = create.outerWallHolder;
                }
            }
        }
        ResizeGroundPlane();
        scriptActive = false;
    }

    void DeleteBorder()
    {
        int childCount = create.outerWallHolder.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(create.outerWallHolder.transform.GetChild(i).gameObject);
        }
    }

    void ResizeGroundPlane()
    {
        Vector3 scaler = new Vector3((float)create.gridSizeX / 10, 1, (float)create.gridSizeZ / 10);
        create.groundPlane.transform.localScale = scaler;
        create.groundPlane.transform.position = new Vector3(create.gridSizeX / 2, 0, create.gridSizeZ / 2);
        create.groundPlane.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(create.gridSizeX, create.gridSizeZ);
    }

    void DeleteInnerWalls()
    {
        int childCount = create.innerWallHolder.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(create.innerWallHolder.transform.GetChild(i).gameObject);
        }
    }

    void BuildInnerWalls()
    {
        if (create.gridSizeX < 5 || create.gridSizeZ < 5)
        {
            Debug.LogWarning("Grid size must be bigger or equal to five");
            return;
        }
        if (create.gridSizeX % 2 == 0 || create.gridSizeZ % 2 == 0)
        {
            Debug.LogWarning("Gridsize must be an uneven number");
            return;
        }

        scriptActive = true;
        int dist = 2;
        for (int i = dist; i <= create.gridSizeX - dist; i++)
        {
            for (int j = dist; j <= create.gridSizeZ - dist; j++)
            {
                if (((i % dist) == 0) && ((j % dist) == 0))
                {
                    GameObject wall = PrefabUtility.InstantiatePrefab(wallPrefab) as GameObject;
                    wall.transform.position = new Vector3(create.start.x + i + create.offset.x,
                                                  create.start.y + create.offset.y, 
                                                  create.start.z + j + create.offset.z);
                    wall.transform.parent = create.innerWallHolder;
                }
            }
        }
        scriptActive = false;

    }

}
