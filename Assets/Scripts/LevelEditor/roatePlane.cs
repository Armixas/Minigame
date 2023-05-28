using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roatePlane : MonoBehaviour
{
    private float roationAngle;
    // Start is called before the first frame update
    void Start()
    {
        roationAngle = 90;
        InvokeRepeating("rotate", 5.0f, 5.0f);
    }

    private void rotate()
    {
        transform.Rotate(roationAngle * Time.time * 1, transform.rotation.y, 0);
        roationAngle += 90;
    }
}
