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
    private Rigidbody _rigidbody;
    private Vector3 playerVelocity;
    private Vector2 movementInput = Vector2.zero;

    private bool isGrounded;
    private bool jumped = false;

    private Vector3 lastPosition;
    private Animator _animator;

    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        lastPosition = transform.position;
        _animator = GetComponentInChildren<Animator>();
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        movementInput = callbackContext.ReadValue<Vector2>();
    }

    public void OnMoveUpDpwn(InputAction.CallbackContext callbackContext)
    {
        movementInput.y = callbackContext.ReadValue<Vector2>().y;
    }

    public void OnMoveLeftRight(InputAction.CallbackContext callbackContext)
    {
        movementInput.x = callbackContext.ReadValue<Vector2>().x;
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

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        _controller.Move(move * (Time.deltaTime * playerSpeed));


        if (jumped && isGrounded)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        playerVelocity.y += gravity * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);

        if (lastPosition != transform.position)
        {
            if (movementInput == Vector2.up)
            {
                _animator.SetFloat("Horizontal", 0);
                _animator.SetFloat("Vertical", 1);
            }
            else if (movementInput == Vector2.down)
            {
                _animator.SetFloat("Horizontal", 0);
                _animator.SetFloat("Vertical", -1);
            }
            else if (movementInput == Vector2.right)
            {
                _animator.SetFloat("Vertical", 0);
                _animator.SetFloat("Horizontal", 1);
            }
            else if (movementInput == Vector2.left)
            {
                _animator.SetFloat("Vertical", 0);
                _animator.SetFloat("Horizontal", -1);
            }
        }
        lastPosition = transform.position;

    }
}
    //public void HitPlayer(Vector3 velocityF, float time)
    //{
    //    _rigidbody.velocity = velocityF;

    //   float  pushForce = velocityF.magnitude;
    //    Vector3 pushDir = Vector3.Normalize(velocityF);
    //    StartCoroutine(Decrease(velocityF.magnitude, time));
    //}
//    private IEnumerator Decrease(float value, float duration)
//    {
//        float delta = 0;
//        delta = value / duration;

//        for (float t = 0; t < duration; t += Time.deltaTime)
//        {
//            yield return null;
//            _rigidbody.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0)); //Add gravity
//        }
//    }
//}
