using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public Vector3 start;
    public Vector3 offset;
    [Header("Prefabs")]
    public GameObject wall;
    public GameObject innerWall;
    public GameObject destroyWall;
    public GameObject groundPlane;
    [Header("PlaceHolder")]
    public Transform outerWallHolder;
    public Transform innerWallHolder;
    public Transform destructableHolder;
    [Header("Grind Size > 5")]
    public int gridSizeX;
    public int gridSizeZ;
    [Header("LayerMask")]
    public LayerMask layerMask;
    
}