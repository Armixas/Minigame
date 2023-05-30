using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class readyPlayer2 : MonoBehaviour
{
    public bool Player1 = false;
    public Button keyboardButton;
    public InputActionReference Number0;
    //public readyPlayer1 player2;

    //public GameObject scene;
    //public GameObject countDown;
    //public GameObject ScoreCanvas;
    private void Start()
    { 
        Number0.action.performed += OnKeyboardActionTriggered;
       // keyboardButton = GetComponent<Button>();
    }

    private void OnKeyboardActionTriggered(InputAction.CallbackContext context)
    {
       
            keyboardButton.onClick.Invoke();// Invoke the button click event
            keyboardButton.interactable = false;
            Player1 = true;
    }

    //private void Update()
    //{
    //    if (Player1 == true && player2.Player1 == true)
    //    {
    //        scene.SetActive(true);
    //        countDown.SetActive(true);
    //        Time.timeScale = 0;
    //        ScoreCanvas.SetActive(false);
    //    }
    //}
}
