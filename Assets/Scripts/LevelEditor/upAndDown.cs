using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upAndDown : MonoBehaviour
{
    public float speed = 1;
    public float range = 1;
    public float speedUp;
    private float defautY;
    public float maxRange;
    // Start is called before the first frame update
    void Start()
    {
        defautY = transform.position.y;
        InvokeRepeating("upSpeed", 5.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

        float y = Mathf.PingPong(Time.time * speed, 1) * range;
        if (y < defautY)
        {
            transform.position = new Vector3(transform.position.x, defautY, transform.position.z);
        }
        else if (y > maxRange)
        {
            transform.position = new Vector3(transform.position.x, maxRange, transform.position.z);
        }
        else
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void upSpeed()
    {
        speed += speedUp;
    }
}

