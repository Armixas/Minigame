using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float speed;
    public float speedUp;

    void Start()
    {
        InvokeRepeating("upSpeed", 5.0f, 5.0f);
    }
    // Update is called once per frame
    void Update()
    {
		transform.Rotate(0f, 0f, speed * Time.deltaTime / 0.01f, Space.Self);
	}

    private void upSpeed()
    {
        if (speed >= 3)
            return;
        else
            speed += speedUp;
    }
}
