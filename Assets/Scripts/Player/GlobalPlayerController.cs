using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalPlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController _controller;
    private Vector3 playerVelocity;
    private Vector2 movementInput = Vector2.zero;
    
    private bool isGrounded;
    private bool jumped = false;

    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
    }
    
    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        movementInput = callbackContext.ReadValue<Vector2>();
    }

    public void DropBomb(InputAction.CallbackContext callbackContext)
    {
        jumped = callbackContext.action.triggered;
    }

    void Update()
    {
        isGrounded = _controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = 0;

        Vector3 move = new Vector3(movementInput.x, 0,  movementInput.y);
        _controller.Move(move * (Time.deltaTime * playerSpeed));

        if (jumped && isGrounded)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        playerVelocity.y += gravity * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);

    }
}
