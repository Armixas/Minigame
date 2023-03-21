using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombermanPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;

    public void DropBomb(InputAction.CallbackContext context)
    {
        Vector3 bombPos = transform.position;
        GameObject.Instantiate(bombPrefab, bombPos, Quaternion.identity);
    }

}
