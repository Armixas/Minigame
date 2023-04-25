using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upAndDown : MonoBehaviour
{
    public float speed = 1;
    public float range = 1;
    public float speedUp;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("upSpeed", 5.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, 1) * range;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void upSpeed()
    {
        speed += speedUp;
    }
}
