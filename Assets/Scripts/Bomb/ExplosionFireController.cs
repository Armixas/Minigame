using System.Collections;
using UnityEngine;

public class ExplosionFireController : MonoBehaviour
{
    private float explosionDelay = 3.0f;
    private int explosionRange = 1;
    private float explosionMultiplier = 3f;
    [SerializeField] private float rectangleSize = 1f;
    [SerializeField] private int explosionDamage = 1;

    private void Start()
    {
        SpawnRectangles();
        StartCoroutine(DestroyDelay(explosionDelay));
    }

    private void SpawnRectangles()
    {
        // Spawn two rectangles perpendicular to each other
        GameObject rectangle1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rectangle1.transform.position = transform.position;
        rectangle1.transform.localScale = new Vector3(rectangleSize, 0.1f, explosionRange * explosionMultiplier);

        GameObject rectangle2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rectangle2.transform.position = transform.position;
        rectangle2.transform.localScale = new Vector3(explosionRange * explosionMultiplier, 0.1f, rectangleSize);
    }

    private IEnumerator DestroyDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}