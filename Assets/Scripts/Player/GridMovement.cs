using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class GridMovement : MonoBehaviour
{
    private bool _isMoving;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    [SerializeField]
    private float moveToTime = 0.25f;
    
    private Animator _animator;
    
    

    private BombermanPlayerController _player;
    private Vector3 _movement;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Start()
    {
        _player = GetComponent<BombermanPlayerController>();
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", 0);
        _animator.speed = 0.75f;
    }
   
    public bool IsPlayerMoving() => _isMoving;

    private bool CanMove(Collider col)
    {
        
        if (col.CompareTag("Immovable") ||  col.CompareTag("Destroyable"))
            return false;
        // If player has boot, they can enter the field
        if(_player.HasBoot() && col.CompareTag("Bomb"))
            return true;
        if (col.CompareTag("Bomb"))
            return false;

        return true;
    }

    public void MoveForward(InputAction.CallbackContext context)
    {
        if (!_isMoving && context.started)
            StartCoroutine(MovePlayer(Vector3.forward));
    }
    public void MoveBack(InputAction.CallbackContext context)
    {
        if (!_isMoving && context.started)
            StartCoroutine(MovePlayer(Vector3.back));
    }
    public void MoveRight(InputAction.CallbackContext context)
    {
        if(!_isMoving && context.started)
            StartCoroutine(MovePlayer(Vector3.right));
    }
    public void MoveLeft(InputAction.CallbackContext context)
    {
        
        if(!_isMoving && context.started)
            StartCoroutine(MovePlayer(Vector3.left));
    }

    
    private IEnumerator MovePlayer(Vector3 direction)
    {
        _isMoving = true;
        
        if (direction == Vector3.forward)
        {
            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", 1);
        }
        else if (direction == Vector3.back)
        {
            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", -1);
        }
        else if (direction == Vector3.right)
        {
            _animator.SetFloat("Vertical", 0);
            _animator.SetFloat("Horizontal", 1);
        }
        else if (direction == Vector3.left)
        {
            _animator.SetFloat("Vertical", 0);
            _animator.SetFloat("Horizontal", -1);
        }

        float elapsedTime = 0.0f;
        _startPosition = transform.position;
        _endPosition = _startPosition + direction;
        
        _player.direction = direction;

        // Detects collisions with objects
        if (Physics.Linecast(_startPosition, _endPosition, out RaycastHit hit) 
            && !CanMove(hit.collider))
        {

            _endPosition = _startPosition + direction;
        }
        else
        {
            while (elapsedTime < moveToTime)
            {
                transform.position = Vector3.Lerp(_startPosition,
                    _endPosition, (elapsedTime / moveToTime));

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = _endPosition;

        }

        _isMoving = false;
    }



}
