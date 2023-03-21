using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float fuseTime = 6f;
    [SerializeField] private int explosionRadius = 1;
    [SerializeField] private int powerUpRadius = 0;
    // Sprite or whatever will be used here
    [SerializeField] private GameObject explosionPrefab;

    private bool _exploded = false;
    // TODO: Bomb needs some kind of ticking animation
    // maybe some kind of scale transformation too

    // Update is called once per frame
    void Update()
    {
        fuseTime -= Time.deltaTime;
        if (fuseTime <= 0 && !_exploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        _exploded = true;
        
        
        //TODO: Player needs to take damage
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        // TODO: Fix as it doesn't seem to work
        // Checks if bomb already exists at current position
        if(other.gameObject.CompareTag("Bomb"))
            Destroy(gameObject);
    }
}
