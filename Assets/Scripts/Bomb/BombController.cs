using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float fuseTime = 4.5f;
    
    // Sprite or whatever will be used here
    [SerializeField] private GameObject explosionPrefab;
    
    private bool _exploded = false;
    
    // TODO: Bomb needs some kind of ticking animation
    // maybe some kind of scale transformation too

    private BombermanPlayerController _player;
    private int _explosionRange;
    
    public int SetExplosionRange(int range) => _explosionRange = range;
    
    private void Awake()
    {
        _player = GetComponentInParent<BombermanPlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        fuseTime -= Time.deltaTime;
        if (fuseTime <= 0 && !_exploded)
        {
            _exploded = true;
            Explode();
        }
    }
    
    private void Explode()
    {
        RaycastHit hit;
        
        BombRaycast();
        //TODO: Player needs to take damage
        gameObject.SetActive(true);
        Destroy(gameObject);
    }

    private void BombRaycast()
    {
        List<Vector3> vectors = new List<Vector3>(){ Vector3.forward, Vector3.back, Vector3.left, Vector3.right, Vector3.up };
        RaycastHit hit;
        for (int i = 0; i < vectors.Count; i++)
        {
            Vector3 endPos = transform.position + vectors[i] * _explosionRange;
            if (Physics.Linecast(transform.position, endPos/*vectors[i] * explosionRadius*/, out hit))
                {
                    if(hit.collider.CompareTag("Destroyable"))
                        Destroy(hit.collider.gameObject);
                }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // TODO: Fix as it doesn't seem to work
        // Checks if bomb already exists at current position
        if(other.gameObject.CompareTag("Bomb"))
            Destroy(gameObject);
    }
}
