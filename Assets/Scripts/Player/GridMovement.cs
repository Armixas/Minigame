using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class GridMovement : MonoBehaviour
{
    private bool _isMoving;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    
    public InputAction moveAction;
    
    [SerializeField]
    private float moveToTime = 0.25f;

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

        float elapsedTime = 0.0f;
        _startPosition = transform.position;
        _endPosition = _startPosition + direction;

        // Detects collisions with objects
        RaycastHit hit;
        if (Physics.Linecast(_startPosition, _endPosition, out hit) && hit.collider.CompareTag("Immovable"))
        {
            
            _endPosition = _startPosition;
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
