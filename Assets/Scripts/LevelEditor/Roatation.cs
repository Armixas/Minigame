using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roatation : MonoBehaviour
{
    public float roationAngle;
    public float speedUp;
    // Start is called before the first frame update
    void Start()
    {
         InvokeRepeating("upSpeed", 5.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, roationAngle * Time.deltaTime); //rotates 50 degrees per second around z axis
       
    }

    private void upSpeed()
    {
        roationAngle += speedUp;
    }
}
