using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class BombermanPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField, Min(0)] private int lifeCount = 3;
    [SerializeField, Min(0)] private int bombExplosionRange = 1; // FireUp++
    [SerializeField, Min(0)] private int bombCount = 1;
    [SerializeField] bool hasBoot = false;

    [SerializeField] private float blinkInterval = 1f;

    private GridMovement _movement;
    private bool _isInvulnerable = false;
    private float _invulnerabilityDuration = 3.2f;
    private Renderer[] _renderers;
    private bool _isBlinking = false;
    
    public Vector3 direction = Vector3.zero;

    public bool HasBoot() => hasBoot;
    public void AddExtendedRange() => bombExplosionRange++;
    public void AddBombCount() => bombCount++;
    public void AddBoot() => hasBoot = true;
    public void KillPlayer() => Destroy(gameObject);

    private void Awake()
    {
        _movement = GetComponent<GridMovement>();
        _renderers = GetComponentsInChildren<Renderer>();
    }

    private void Start()
    {
        //_movement = GetComponent<GridMovement>();
        //_renderers = GetComponentsInChildren<Renderer>();
    }

    public void DropBomb(InputAction.CallbackContext context)
    {
        if (_movement.IsPlayerMoving() || bombCount < 1)
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

    public void DecrementHealth()
    {
        if (_isInvulnerable)
            return;

        lifeCount--;

        if (lifeCount <= 0)
            Destroy(gameObject);
        else
            StartCoroutine(ActivateInvulnerability());
    }

    private IEnumerator ActivateInvulnerability()
    {
        _isInvulnerable = true;
        StartCoroutine(BlinkPlayer());
        yield return new WaitForSeconds(_invulnerabilityDuration);
        StopCoroutine(BlinkPlayer());
        SetPlayerVisible(true);
        _isInvulnerable = false;
    }

    private IEnumerator BlinkPlayer()
    {
        _isBlinking = true;

        while (_isInvulnerable)
        {
            SetPlayerVisible(false);
            yield return new WaitForSeconds(blinkInterval);
            SetPlayerVisible(true);
            yield return new WaitForSeconds(blinkInterval);
        }

        _isBlinking = false;
    }

    private void SetPlayerVisible(bool isVisible)
    {
        foreach (Renderer renderer in _renderers)
        {
            renderer.enabled = isVisible;
        }
    }

    public Vector3 GetMovementDirection()
    {
        return direction;
    }

    private void OnDestroy()
    {
        // TODO: Add death animations and additional logic if needed
    }
}
