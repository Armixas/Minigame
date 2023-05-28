using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physics : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public float thrust = 10;
    public float addFroce = 5;
    private float downForce = 10;

    private bool movingup = true;
    private float defaultY;

    public float maxRise = 1;
    public float maxFall = 0;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        defaultY = transform.position.y;

        GameObject TurnTable = GameObject.FindGameObjectWithTag("TunTable");
        Physics.IgnoreCollision(TurnTable.GetComponent<BoxCollider>(), GetComponent<BoxCollider>());
    }

    private void Update()
    {
        if (transform.position.y >= maxRise)
            movingup = false;
        if (transform.position.y <= maxFall)
            movingup = true;

        InvokeRepeating("upForse", 5.0f, 5.0f);
    }

    private void FixedUpdate()
    {
        if (movingup)
        {
            MoveUp(thrust);
        }
        else
        {
            MoveUp(-downForce);
        }

        ClampPosition();
    }

    private void MoveUp(float thrust)
    {
        Vector3 upForce = Vector3.up * thrust;
        _rigidbody.AddForce(upForce, ForceMode.Impulse);
    }

    private void MoveDown(float thrust)
    {
        Vector3 upForce = Vector3.up * thrust;
        _rigidbody.AddForce(upForce);
    }
    private void ClampPosition()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = Mathf.Clamp(currentPosition.y, maxFall, maxRise);
        transform.position = currentPosition;
    }

    private void upForse()
    {
        thrust = +addFroce;
    }
}
