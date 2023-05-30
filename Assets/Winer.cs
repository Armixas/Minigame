using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winer : MonoBehaviour
{
    public GameObject[] characters;
    public int playerNumber = -1;

    void Awake()
    {

        StartCoroutine(Wait());
    }

    private void Update() { }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("BomberMan", LoadSceneMode.Single);
    }

}

//            //health.changeDeath(3);

//            if (GetComponent<OnSpawn>().playerNumber == 1)
//            {
//                if (loser == false)
//                { 
//                    PlayerPrefs.SetInt("PlayerScore2", PlayerPrefs.GetInt("PlayerScore2") + 1);
//                    loser = true;
//                }
//                Instantiate(winingHead);
//                winingHead.GetComponent<Winer>().wining(2);
//                StartCoroutine(Wait());
//            }
//            else if (GetComponent<OnSpawn>().playerNumber == 2)
//            {
//                if (loser == false)
//                {
//                    PlayerPrefs.SetInt("PlayerScore1", PlayerPrefs.GetInt("PlayerScore1") + 1);
//                    loser = true;
//                }
//                Instantiate(winingHead);
//                winingHead.GetComponent<Winer>().wining(1);
//                StartCoroutine(Wait());
//            }
//        }

//    }
//}

//public void wining(int playerNumber)
//{
//    int selectedCharacter = PlayerPrefs.GetInt("SelecetedCharacter" + playerNumber);
//    // Debug.Log(selectedCharacter);
//    // selectedCharacter = (selectedCharacter + 1) % characters.Length;
//    characters[playerNumber].SetActive(true);
//}

