using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour
{
    private bool firstDeath = false;
    private bool secondDeath = false;
    private bool thirdeath = false;
    public GameObject winingHead1;
    public GameObject winingHead2;

    private Vector3 spawnPoint;
    private SetHealth health;

    public bool loser =false;
    private bool assignSpawn = false;
    private int number;

    void Awake()
    {
        //spawnPoint = transform.position;
        //spawnPoint.y = transform.position.y + 0.5f;


    }
    void Update()
    {
        if (assignSpawn == false)
        {
            if (GetComponent<OnSpawn>().playerNumber == 1)
            {
                spawnPoint = new Vector3(-2.25f, 1.15f, -2);
            }
            else
                spawnPoint = new Vector3(2.25f, 1.15f, -2);
            assignSpawn = true;
        }
        if (health == null)
        {
            GameObject healGameobject = GetComponent<OnSpawn>().healthGameObject;
            health = healGameobject.GetComponent<SetHealth>();
        }
        else
        { }
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
                health.changeDeath(1);
            }
            else if (secondDeath == false)
            {
                secondDeath = true;
                health.changeDeath(2);
            }
            else if (thirdeath == false)
            {
                thirdeath = true;
                health.changeDeath(3);

                if (GetComponent<OnSpawn>().playerNumber == 1)
                {
                    if (loser == false)
                    {
                        PlayerPrefs.SetInt("PlayerScore2", PlayerPrefs.GetInt("PlayerScore2") + 1);
                        number = 1;
                        loser = true;
                        Instantiate(winingHead2);
                    }
                }
                else if (GetComponent<OnSpawn>().playerNumber == 2)
                {
                    if (loser == false)
                    {
                        PlayerPrefs.SetInt("PlayerScore1", PlayerPrefs.GetInt("PlayerScore1") + 1);
                        number = 0;
                        loser = true;
                        Instantiate(winingHead1);
                    }
                }
                //winingHead.GetComponent<Winer>().playerNumber = number;
                //winingHead.GetComponent<Winer>().wining(2);
                GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Player");

                foreach (GameObject go in gameObjectArray)
                {
                    go.SetActive(false);
                }
            }
        }
    }
}
