using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombermanPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    
    [SerializeField, Min(0)] private int extendedRange = 0; // FireUp
    [SerializeField, Min(1)] private int bombCount = 1;
    [SerializeField] private bool hasBoot = false;

    public void DropBomb(InputAction.CallbackContext context)
    {
        Vector3 bombPos = transform.position;
        GameObject.Instantiate(bombPrefab, bombPos, Quaternion.identity);
    }

    public void AddExtentedRange() => extendedRange++;
    public void AddBombCount() => bombCount++;
    public void AddBoot() => hasBoot = true;

    public void KillPlayer()
    {
        Debug.Log("KillPlayerNotInmplemented");
    }

    
}
