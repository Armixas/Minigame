using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDeath : MonoBehaviour
{
    public GameObject newgameObject;
    public bool firstDeath = false;
    public bool secondDeath = false;
    public bool thirdeath = false;

    private Vector3 spawnPoint;

    void Awake()
    {
        spawnPoint = transform.position;
        spawnPoint.y = transform.position.y + 0.5f;
    }
    void Update()
    {
        WipeoutDeath();
    }

    void WipeoutDeath()
    {
        if (transform.position.y < 0)
        {
            transform.position = spawnPoint;
            if (firstDeath == false)
            {
                firstDeath = true;
            }
            else if (secondDeath == false)
            {
                secondDeath = true;
            }
            else if (thirdeath == false)
            {
                thirdeath = true;
            }
        }
    }
}
