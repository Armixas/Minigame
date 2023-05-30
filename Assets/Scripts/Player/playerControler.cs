using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerControler : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float maxVelocityChange = 10.0f;

    private sceneManager sceneManager;

    private Rigidbody _rigidbody;
    private Vector3 playerVelocity;
    private Vector3 moveDir;
    private Vector2 movementInput = Vector2.zero;

    private bool jumped = false;
    private Vector3 lastPosition;
    private Animator _animator;

    private float distToGround;

    //for stun
    //private bool canMove = true; //If player is not hitted
    //private bool isStuned = false;
    //private bool wasStuned = false; //If player was stunned before get stunned another time
    private float pushForce;
    private Vector3 pushDir;


    private void Awake()
    { 

    }

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        lastPosition = transform.position;
        _animator = GetComponentInChildren<Animator>();

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        distToGround = GetComponent<Collider>().bounds.extents.y;

        sceneManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<sceneManager>();
    }

    public void OnMoveUpDown(InputAction.CallbackContext callbackContext)
    {
        movementInput.y = callbackContext.ReadValue<Vector2>().y;
    }

    public void OnMoveLeftRight(InputAction.CallbackContext callbackContext)
    {
        movementInput.x = callbackContext.ReadValue<Vector2>().x;
    }

    public void OnJumping(InputAction.CallbackContext callbackContext)
    {
        jumped = callbackContext.action.triggered;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.2f);
    }

    void FixedUpdate()
    {
        if (sceneManager.CanMove == true)
        {
            if (IsGrounded())
            {
                // Calculate how fast we should be moving
                moveDir = new Vector3(movementInput.x, 0, movementInput.y);
                moveDir = moveDir.normalized;
                Vector3 targetVelocity = moveDir;
                targetVelocity *= playerSpeed;
                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = _rigidbody.velocity;
                if (targetVelocity.magnitude < velocity.magnitude) //If I'm slowing down the character
                {
                    targetVelocity = velocity;
                    _rigidbody.velocity /= 1.1f;
                }
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;
                if (Mathf.Abs(_rigidbody.velocity.magnitude) < playerSpeed * 1.0f)
                    _rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

                //Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
                //_rigidbody.(move * (Time.deltaTime * playerSpeed));


                if (jumped)
                {
                    _rigidbody.velocity = new Vector3(velocity.x, Mathf.Sqrt(2 * jumpHeight * gravity), velocity.z);
                }

                // Jump
                //if (jumped)
                //{
                //    _rigidbody.velocity = Vector3.zero;
                //    _rigidbody.angularVelocity = Vector3.zero;
                //    _rigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
                //    jumped = false;
                //}
            }
            else
                _rigidbody.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));

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

    public void HitPlayer(Vector3 velocityF, float time)
    {
        _rigidbody.velocity = velocityF;

        pushForce = velocityF.magnitude;
        pushDir = Vector3.Normalize(velocityF);
        StartCoroutine(Decrease(velocityF.magnitude, time));
    }

    private IEnumerator Decrease(float value, float duration)
    {
        //  if (isStuned)
        //      wasStuned = true;
        //  isStuned = true;
        ////  canMove = false;

        float delta = 0;
        delta = value / duration;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            yield return null;
                pushForce = pushForce - Time.deltaTime * delta;
                pushForce = pushForce < 0 ? 0 : pushForce;
                //Debug.Log(pushForce);
            _rigidbody.AddForce(new Vector3(0, GetComponent<Rigidbody>().mass, 0)); //Add gravity
        }

        //if (wasStuned)
        //{
        //    wasStuned = false;
        //}
        //else
        //{
        //    isStuned = false;
        //    canMove = true;
        //}
    }
}
