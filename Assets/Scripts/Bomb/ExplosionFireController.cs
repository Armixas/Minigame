using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFireController : MonoBehaviour
{
    public float _explosionDelay = 3.0f;
    private float _destryDely = 3.0f;
    [Min(1)]private int _explosionRange = 1;
    [SerializeField]private float _explosionMultiplier = 3f;

    [SerializeField, Min(1)] private int explosionDamage = 1;

    private Vector3 _explosionRadius;
    
    private void OnTriggerEnter(Collider other) => DealDamageToPlayers();
    private void OnTriggerStay(Collider other) => DealDamageToPlayers();
    
    void Start()
    {
        DealDamageToPlayers();
        StartCoroutine(DestroyDelay(_destryDely));
    }

    public void SetFireRadius(string axis, int raycastLength)
    {
        if(raycastLength == 0) return;
        
        var scale = transform.localScale;

        var centerOffset = 0.75f;

        float explosionDist = centerOffset + raycastLength * _explosionMultiplier * 0.5f;

        if (axis == "center")
        {
            transform.localScale = new Vector3(
                0.8f,
                0.8f,
                0.8f);
        }
        else if (axis == "x")
        {
            transform.localScale = new Vector3(
                scale.x * explosionDist,
                scale.y,
                scale.z);
            transform.localPosition += new Vector3(centerOffset, 0f, 0f);
        }
        else if (axis == "z")
       {
           transform.localScale = new Vector3(
               scale.x,
               scale.y,
               scale.z * explosionDist);
            transform.localPosition += new Vector3(0f, 0f, centerOffset);
        }
       else if (axis == "-x")
       {
           transform.localScale = new Vector3(
               scale.x * explosionDist,
               scale.y,
               scale.z);
           transform.localPosition += new Vector3(-centerOffset, 0f, 0f);
       }
       else if (axis == "-z")
       {
           transform.localScale = new Vector3(
               scale.x,
               scale.y,
               scale.z * explosionDist);
           transform.localPosition += new Vector3(0f, 0f, -centerOffset);
       }
    }
    
    private void DealDamageToPlayers()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, _explosionRadius);

        foreach (Collider col in colliders)
        {
            BombermanPlayerController player = col.gameObject.GetComponent<BombermanPlayerController>();

            if (col.gameObject.CompareTag("Bomb"))
            {
               BombController bomb = col.gameObject.GetComponent<BombController>();
               bomb.Explode();
               
            }
            if (player != null)
            {
                Vector3 playerRelativePosition = player.transform.position - transform.position;
                bool isInRange = Mathf.Abs(playerRelativePosition.x) <= _explosionRange && Mathf.Abs(playerRelativePosition.z) <= _explosionRange;
                
                bool isDiagonal = Mathf.Abs(playerRelativePosition.x) > 0 && Mathf.Abs(playerRelativePosition.z) > 0;

                if (isInRange && !isDiagonal)
                {
                    bool obstacleInPath = CheckObstacleInPath(player.transform.position, playerRelativePosition.normalized);

                    if (!obstacleInPath)
                    {
                        player.DecrementHealth();
                    }
                }
            }
        }
    }

    private bool CheckObstacleInPath(Vector3 playerPosition, Vector3 direction)
    {
        float distance = Vector3.Distance(transform.position, playerPosition);

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Destroyable") || hit.collider.CompareTag("Immovable"))
            {
                return true;
            }
        }

        return false;
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
