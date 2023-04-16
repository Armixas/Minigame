using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraHolder;
    public Vector3 offset;

    /*public override void onStartAuthority()
    {
        cameraHolder.SetActive(true);
    }*/
    // Update is called once per frame
    public void Update()
    {
        cameraHolder.transform.position = transform.position + offset;
    }
}
