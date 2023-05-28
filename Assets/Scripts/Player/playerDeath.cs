using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDeath : MonoBehaviour
{
    public GameObject newgameObject;
    void Update()
    {
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(-2, 2, -1.4f);
        }
    }
}
