using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countDown : MonoBehaviour
{
    Text text;
    public bool lastChange = false;
    void Awake()
    {
        text = GetComponent<Text>();
        text.text = "3";
        InvokeRepeating("ChangeText", 1f, 1f);
    }

    void ChangeText()
    {
        if (text.text == "3")
        {
            text.text = "2";
        }
        else if (text.text == "2")
        {
            text.text = "1";
        }
        else if (text.text == "1")
        {
            text.text = "Go!";
        }
        else if (text.text == "Go!")
        {
            lastChange = true;
        }
    }
}

    // Update is called once per frame
