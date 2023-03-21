using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverAndRotate : MonoBehaviour
{
    [SerializeField] private float hoverDist = 0.01f;
    [SerializeField] private float hoverSpeed = 0.2f;
    void Update()
    {
        Vector3 hoverpos = new Vector3(
            transform.position.x,
            transform.position.y + Mathf.Sin(Time.time * hoverSpeed) * hoverDist,
            transform.position.z);
        
        transform.position = hoverpos;
        
        transform.Rotate(new Vector3(0, 25, 0) * Time.deltaTime, Space.Self);
        transform.Rotate(new Vector3(0, 35, 0) * Time.deltaTime, Space.World);
    }
}
