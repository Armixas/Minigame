using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(GridMovement))]
public class BombermanPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    
    [SerializeField, Min(0)] private int bombExplosionRange = 1; // FireUp++
    [SerializeField, Min(1)] private int bombCount = 1;
    [SerializeField] bool hasBoot = false;
    
    
    private GridMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<GridMovement>();
    }

    public void DropBomb(InputAction.CallbackContext context)
    {
        if (_movement.IsPlayerMoving())
            return;
        // sutaisytas trigubinimasis
        if (context.started)
        {
            Vector3 bombPos = transform.position;
            GameObject bomb = GameObject.Instantiate(bombPrefab, bombPos, Quaternion.identity);
            bomb.GetComponent<BombController>().SetExplosionRange(bombExplosionRange); 
        }
    }

    public int GetExplosionRange() => bombExplosionRange;
    public bool HasBoot() => hasBoot;
    public void AddExtendedRange() => bombExplosionRange++;
    public void AddBombCount() => bombCount++;
    public void AddBoot() => hasBoot = true;

    public void KillPlayer()
    {
        // TODO: Add death animations and aditional logic if needed
        Debug.Log("KillPlayerNotInmplemented");
    }

    
}
