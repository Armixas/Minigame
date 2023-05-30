using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setHead : MonoBehaviour
{
   [SerializeField] private int playerNumber;
    public GameObject[] characters;

    private void Start()
    {
       int selectedCharacter = PlayerPrefs.GetInt("SelecetedCharacter" + playerNumber);
       // Debug.Log(selectedCharacter);
       // selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
    }
}
