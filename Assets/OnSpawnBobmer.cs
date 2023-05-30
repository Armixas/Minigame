using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OnSpawnBobmer : MonoBehaviour
{
    [SerializeField] private GameObject[] models;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    [SerializeField] private GameObject Zombie;
    [SerializeField] private GameObject Tiger;
    private GameObject left;
    private GameObject right;
    public int playerNumber;

    private PlayerInput playerInput;
    private GameObject healthGameObject;

    private void Awake()
    {
        if (GetComponent<GridMovement>().enabled == false)
        {
            GetComponent<GridMovement>().enabled = true;
        }
        right = GameObject.Find("Ready");
        left = GameObject.Find("Ready1");
        Button leftButton = left.GetComponent<Button>();

        Button rightButton = right.GetComponent<Button>();
        playerInput = GetComponent<PlayerInput>();
        InputActionMap actionMap = playerInput.currentActionMap;

        if (string.Join("\n", actionMap.FindAction("MoveForward").controls).Contains("Left"))
        {


            ////player1
            //int selectedCharacter = PlayerPrefs.GetInt("SelecetedCharacter" + 1);
            //GameObject model = Instantiate(models[selectedCharacter]);
            //model.transform.position = Vector3.zero;
            //model.transform.parent = transform;
            //transform.position = spawnPoint1.transform.position;

            leftButton.interactable = false;

            healthGameObject = GameObject.Find("Player1");
            playerNumber = 1;
        }
        else
        {
            Debug.Log("Player2");
            //player2
            //int selectedCharacter = PlayerPrefs.GetInt("SelecetedCharacter" + 2);
            //GameObject model = Instantiate(models[selectedCharacter]);
            //model.transform.position = Vector3.zero;
            //model.transform.parent = transform;
            transform.position = spawnPoint2.transform.position;

            rightButton.interactable = false;

            healthGameObject = GameObject.Find("Player2");
            playerNumber = 2;
        }

    }
}
