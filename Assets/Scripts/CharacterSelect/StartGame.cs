using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour

{
    public int playerNumber;
    private bool canStartNewGame;
    public GameObject button;
    // Start is called before the first frame update
    void Update()
    {
        int i = 1;
        while (i < playerNumber)
        {
            if (PlayerPrefs.GetInt("SelecetedCharacter" + i) == -1)
            {
                canStartNewGame = false;
                break;
            }
            else
            {
                canStartNewGame = true;
            }
            i++;

        }
        //defaultColor = button.GetComponent<Image>().color;
    }

    public void startGame()
    {
        if (canStartNewGame == true)
        {
            PlayerPrefs.SetInt("PlayerNumber", playerNumber);
            SceneManager.LoadScene("BomberMan", LoadSceneMode.Single);
        }
    }
}
