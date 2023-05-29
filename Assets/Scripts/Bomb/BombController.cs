using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private float fuseTime = 4.5f;
    [SerializeField] private GameObject explosionPrefab;
    
    private bool _exploded = false;
    private int _explosionRange;
    private Transform _bombTransform;
    private Vector3 _initialScale;
    private float _scaleMultiplier = 1.5f;

    public BombermanPlayerController player;

    public int SetExplosionRange(int range) => _explosionRange = range;

    private void Awake()
    {
        _bombTransform = transform;
        _initialScale = _bombTransform.localScale;
    }

    private void Start()
    {
        StartCoroutine(ScaleBomb());
    }

    private IEnumerator ScaleBomb()
    {
        float scaleTime = fuseTime / 2f;
        float elapsedTime = 0f;

        while (elapsedTime < scaleTime)
        {
            float t = elapsedTime / scaleTime;
            float scale = Mathf.Lerp(1f, _scaleMultiplier, t);
            _bombTransform.localScale = _initialScale * scale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _bombTransform.localScale = _initialScale * _scaleMultiplier;

        yield return new WaitForSeconds(fuseTime - scaleTime);

        Explode();
    }

    private void Explode()
    {
        InitialExplosion();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
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
        if (other.gameObject.CompareTag("Bomb"))
        {
            var go = gameObject;
            // go.SetActive(false);
            // Destroy(go);
        }
    }
}
