using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] characters;
    public int selectedCharacter = 0;
    public int playerNumber;
    public Button button;
    private Color defaultColor;

    public void Start()
    {
        int i = 1;
        while (i < 4)
        {
            PlayerPrefs.SetInt("SelecetedCharacter" + i, -1);
            i++;
        }
        defaultColor = button.GetComponent<Image>().color;
    }
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter + 1) % characters.Length;
        characters[selectedCharacter].SetActive(true);
        button.GetComponent<Image>().color = defaultColor;
        button.interactable = true;
    }

    public void PreviosCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter += characters.Length;
        }
        characters[selectedCharacter].SetActive(true);
        button.GetComponent<Image>().color = defaultColor;
        button.interactable = true;
    }
    public void SetCharacter()
    {
        int i = 1;
        while(i < 4)
        {
            if (selectedCharacter == PlayerPrefs.GetInt("SelecetedCharacter" + i) && i != playerNumber)
            {
                button.GetComponent<Image>().color = Color.red;
                break;
            }
            else
            {
               PlayerPrefs.SetInt("SelecetedCharacter" + playerNumber, selectedCharacter);
               button.interactable = false;
            }
            i++;
        }
    }
}
