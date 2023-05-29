using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private GameObject characterSelect;
    [SerializeField] private GameObject mainmenu;
    public void quit()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Quit?");
        Application.Quit();
    }

    public void CharacterSelect()
    {
        mainmenu.SetActive(false);
        characterSelect.SetActive(true);
    }
}
