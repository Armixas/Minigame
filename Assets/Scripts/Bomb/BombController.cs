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
    public BombermanPlayerController bootPlayer;
    private List<int> _raycastLength;
    private int _triggeredTimes = 0;

    public int SetExplosionRange(int range) => _explosionRange = range;

    private void Awake()
    {
        _bombTransform = transform;
        _initialScale = _bombTransform.localScale;
    }

    private void Start()
    {
        _raycastLength = new List<int>();
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

    public void Explode()
    {
        InitialExplosion();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // z, -z, -x, x
        InstantiateFire("z", _raycastLength[0]);
        InstantiateFire("-z", _raycastLength[1]);
        
        InstantiateFire("x", _raycastLength[3]);
        InstantiateFire("-x", _raycastLength[2]);
        InstantiateFire("center", 0);
        player.AddBombCount();
    }

    private void InstantiateFire(string axis, int raycastLength)
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        ExplosionFireController fire = explosion.GetComponent<ExplosionFireController>();
        explosion.transform.SetParent(transform.parent);
        fire.SetFireRadius(_explosionRange);
        fire.SetFireRadius(axis, raycastLength);
    }

    private void InitialExplosion()
    {                                               // z, -z, -x, x
        List<Vector3> vectors = new List<Vector3>(){ Vector3.forward, Vector3.back, Vector3.left, Vector3.right};
        RaycastHit hit;
        for (int i = 0; i < vectors.Count; i++)
        {
            Vector3 endPos = transform.position + vectors[i] * _explosionRange;
            if (Physics.Linecast(transform.position, endPos, out hit))
            {
                if (hit.collider.CompareTag("Destroyable"))
                {
                    int hitDist = (int) Math.Ceiling(hit.distance);
                    _raycastLength.Add(hitDist);
                    Destroy(hit.collider.gameObject);
                    continue;
                }

                if (hit.collider.CompareTag("Immovable"))
                {
                    int hitDist = (int)Math.Ceiling(hit.distance);
                    if(hitDist != 0)
                        hitDist -= 1;
                    
                    _raycastLength.Add(hitDist);
                    continue;
                }
            }
            _raycastLength.Add(_explosionRange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggeredTimes == 0)
        {
            _triggeredTimes++;
            return;
            
        }
        if (other.CompareTag("Player"))
        {
            bootPlayer = other.GetComponent<BombermanPlayerController>();
            if (bootPlayer != null && bootPlayer.HasBoot())
            {
                StartCoroutine(SlideBomb(bootPlayer.direction, 0.3f));
            }
        }
    }

    public IEnumerator SlideBomb(Vector3 direction, float slideDuration)
    {
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + direction;
        float elapsedTime = 0f;
        
        Collider[] colliders = Physics.OverlapSphere(targetPosition, 0.5f);
        bool canSlide = true;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Destroyable") || collider.CompareTag("Player")
                                                   || collider.CompareTag("Immovable")
                                                   || collider.CompareTag("Bomb"))
            {
                canSlide = false;
                break;
            }
        }

        if (canSlide)
        {
            while (elapsedTime < slideDuration)
            {
                float t = elapsedTime / slideDuration;
                transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
        }
    }


}
