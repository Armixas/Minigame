using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFireController : MonoBehaviour
{
    private float _explosionDelay = 3.0f;
    [Min(1)]private int _explosionRange = 1;
    [SerializeField]private float _explosionMultiplier = 3f;

    [SerializeField, Min(1)] private int explosionDamage = 1;

    private Vector3 _explosionRadius;
    
    private void OnTriggerEnter(Collider other) => DealDamageToPlayers();
    
    void Start()
    {
        DealDamageToPlayers();
        StartCoroutine(DestroyDelay(_explosionDelay));
    }

    public void SetFireRadius(string axis)
    {
        var scale = transform.localScale;
        if(axis == "x")
            transform.localScale = new Vector3(
                scale.x * _explosionRange * _explosionMultiplier,
                scale.y,
                scale.z);
        if(axis == "z")
            transform.localScale = new Vector3(
                scale.x,
                scale.y,
                scale.z * _explosionRange * _explosionMultiplier);
    }
    
    private void DealDamageToPlayers()
    {
        // Get all colliders within explosion radius
        Collider[] colliders = Physics.OverlapBox(transform.position, _explosionRadius);

        foreach (Collider col in colliders)
        {
            // Get the BombermanPlayerController component from the collider's GameObject
            BombermanPlayerController player = col.gameObject.GetComponent<BombermanPlayerController>();
            
            if (player != null)
            {
                player.DecrementHealth();
            }
        }
    }

    private IEnumerator DestroyDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void SetFireRadius(int radius)
    {
        _explosionRange = radius;
        _explosionRadius = new Vector3(_explosionRange, 0, _explosionRange);
    } 
}
