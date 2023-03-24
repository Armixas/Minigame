using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BombermanPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField, Min(0)] private int lifeCount = 3;
    [SerializeField, Min(0)] private int bombExplosionRange = 1; // FireUp++
    [SerializeField, Min(0)] private int bombCount = 1;
    [SerializeField] bool hasBoot = false;
    
    
    private GridMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<GridMovement>();
    }

    public void DropBomb(InputAction.CallbackContext context)
    {
        if (_movement.IsPlayerMoving()
            || bombCount < 1)
            return;

        if (context.started)
        {
            Vector3 bombPos = transform.position;
            GameObject bomb = GameObject.Instantiate(bombPrefab, bombPos, Quaternion.identity);
            var bombScript = bomb.GetComponent<BombController>();
            bombScript.SetExplosionRange(bombExplosionRange);
            bombScript.player = this;
            bombCount--;
        }
    }
    public bool HasBoot() => hasBoot;
    public void AddExtendedRange() => bombExplosionRange++;
    public void AddBombCount() => bombCount++;
    public void AddBoot() => hasBoot = true;
    public void KillPlayer() => Destroy(gameObject);
    public void DecrementHealth()
    {
        lifeCount--;
        if (lifeCount <= 0)
            Destroy(gameObject);
    }
    private void OnDestroy()
    {
        // TODO: Add death animations and additional logic if needed
    }
}
