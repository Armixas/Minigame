using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OnSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] models;
    public Vector3 spawnPoint1 = new Vector3(-2.25f, 1.15f, -2);
    public Vector3 spawnPoint2 = new Vector3(2.25f, 1.15f, -2);
    //[SerializeField] private GameObject Zombie;
    //[SerializeField] private GameObject Tiger;
    private GameObject left;
    private GameObject right;

    private PlayerInput playerInput;
    private GameObject healthGameObject;

    private void Awake()
    {
        right = GameObject.Find("Ready");
        left = GameObject.Find("Ready1");
        Button leftButton = left.GetComponent<Button>();
        
        Button rightButton = right.GetComponent<Button>();
        playerInput = GetComponent<PlayerInput>();
        InputActionMap actionMap = playerInput.currentActionMap;

        if (string.Join("\n", actionMap.FindAction("Up").controls).Contains("Left"))
        {
            
            //player1
            int selectedCharacter = PlayerPrefs.GetInt("SelecetedCharacter" + 1);
            GameObject model = Instantiate(models[selectedCharacter]);
            model.transform.parent = transform;
            transform.position = spawnPoint1;

            leftButton.interactable = false;
            Debug.Log(leftButton.interactable);
        }
        else
        {
            //player2
            int selectedCharacter = PlayerPrefs.GetInt("SelecetedCharacter" + 2);
            GameObject model = Instantiate(models[selectedCharacter]);
            model.transform.parent = transform;
            transform.position = spawnPoint2;
            rightButton.interactable = false;
        }

    }
}
