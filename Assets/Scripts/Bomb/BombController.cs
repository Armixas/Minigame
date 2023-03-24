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
    private int _explosionRange;
    
    
    public BombermanPlayerController player;
    
    public int SetExplosionRange(int range) => _explosionRange = range;



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
        
        InitialExplosion();
        
        //TODO: Player needs to take damage
        gameObject.SetActive(true);
        Destroy(gameObject);
        
    }


    private void OnDestroy()
    {
        // Spawns explosion fire
        InstantiateFire("x");
        InstantiateFire("z");
        
        player.AddBombCount();
    }

    private void InstantiateFire(string axis)
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ExplosionFireController fire = explosion.GetComponent<ExplosionFireController>();
        fire.SetFireRadius(_explosionRange);
        fire.SetFireRadius(axis);
    }

    private void InitialExplosion()
    {
        List<Vector3> vectors = new List<Vector3>(){ Vector3.forward, Vector3.back, Vector3.left, Vector3.right};
        RaycastHit hit;
        for (int i = 0; i < vectors.Count; i++)
        {
            Vector3 endPos = transform.position + vectors[i] * _explosionRange;
            if (Physics.Linecast(transform.position, endPos, out hit))
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
        if (other.gameObject.CompareTag("Bomb"))
        {
            var go = gameObject;
            
            //go.SetActive(false);
            //Destroy(go);
        }
    }
}