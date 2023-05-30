using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHealth : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    public GameObject[] characters;
    private int selectedCharacter;

    private void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt("SelecetedCharacter" + playerNumber);
        // Debug.Log(selectedCharacter);
        // selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
        characters[selectedCharacter + 2].SetActive(true);
        characters[selectedCharacter + 4].SetActive(true);
    }

    public void changeDeath(int a)
    {
        switch (a)
        {
            case 1:
                characters[selectedCharacter].SetActive(false);
                break;
            case 2:
                characters[selectedCharacter+2].SetActive(false);
                break;
            case 3:
                characters[selectedCharacter + 4].SetActive(false);
                break;
        }
    }
}
