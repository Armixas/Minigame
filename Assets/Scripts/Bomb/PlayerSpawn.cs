using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject[] players;
    public Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        int i = 1;
        while (i < PlayerPrefs.GetInt("PlayerNumber"))
        {
            players[i].transform.position = spawnPoints[i].position;
            i++;
        }
    }

}
