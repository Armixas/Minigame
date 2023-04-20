using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    [SerializeField] bool freezeXZAxis = true;
    // Update is called once per frame
    private void LateUpdate()
    {
        Transform transform = Camera.main.transform;
        if (freezeXZAxis)
        {
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = transform.rotation;
        } 
            
    }
}
