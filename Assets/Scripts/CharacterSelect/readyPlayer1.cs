using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class readyPlayer1 : MonoBehaviour
{
    public bool Player1 = false;
    public Button keyboardButton;
    public InputActionReference keyboardAction;

    private void Start()
    {
        keyboardAction.action.performed += OnKeyboardActionTriggered;
    }

    private void OnKeyboardActionTriggered(InputAction.CallbackContext context)
    {
            keyboardButton.onClick.Invoke();// Invoke the button click event
            Player1 = true;
    }

    public void Press()
    {
        keyboardButton.enabled = false;
    }

    public void setFalse()
    {
        Player1 = false;
    }
}
