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
        if (PlayerPrefs.GetInt("SelecetedCharacter1") > -1 && PlayerPrefs.GetInt("SelecetedCharacter2") > -1)
        {
            canStartNewGame = true;
        }
        else
            canStartNewGame = false;
        //int i = 0;
        //while (i < playerNumber)
        //{
        //    if (PlayerPrefs.GetInt("SelecetedCharacter1" + i) == -1)
        //    {
        //        canStartNewGame = false;
        //        break;
        //    }
        //    else
        //    {
        //        canStartNewGame = true;
        //    }
        //    i++;

            //}
            ////defaultColor = button.GetComponent<Image>().color;
    }

    public void startGame()
    {
        if (canStartNewGame == true)
        {
            PlayerPrefs.SetInt("PlayerNumber", playerNumber);
            SceneManager.LoadScene("Wipeout", LoadSceneMode.Single);
            //SceneManager.LoadScene("BomberMan", LoadSceneMode.Single);
        }
    }
}
