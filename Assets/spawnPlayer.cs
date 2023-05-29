using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class spawnPlayer : MonoBehaviour
{
    private GameObject playerPrefab;
    private bool jumped;
    // Start is called before the first frame update
    void Start()
    {

        playerPrefab = GetComponent<PlayerInputManager>().playerPrefab;
    }

    public void OnSpawn()
    {
        //playerPrefab.transform.position = new Vector3(-2, 1, -3);

    }

}

    // Update is called once per frame
