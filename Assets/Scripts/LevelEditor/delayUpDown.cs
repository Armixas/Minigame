using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delayUpDown : MonoBehaviour
{
    // Start is called before the first frame update
    public int waitTime=5;
    void Start()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<upAndDown>().enabled = true;
    }

}
