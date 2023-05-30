using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreSet : MonoBehaviour
{
    Text score;
    public int playerNumber;
    void Start()
    {
        score = GetComponent<Text>();
        score.text = "Score : " + PlayerPrefs.GetInt("PlayerScore" + playerNumber , 0);
    }
}
