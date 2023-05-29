using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSpawn : MonoBehaviour
{
    [SerializeField] private GameObject Zombie;
    [SerializeField] private GameObject Tiger;

    private void Awake()
    {
        GameObject model = Instantiate(Zombie);
    model.transform.parent = transform;
        transform.position = new Vector3(-2, 5, -3);
    }
}
