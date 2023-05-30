using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupDrop : MonoBehaviour
{
    [SerializeField] private List<GameObject> pickUps;
    private readonly float _dropChance = 0.3f;

    private void OnDestroy()
    {
        if(pickUps.Count == 0) return;
        DropPickup();
    }

    private void DropPickup()
    {
        float randomValue = Random.value;
        int pickupIndex = Random.Range(0, pickUps.Count);

        if (randomValue <= _dropChance)
        {
            GameObject pickup = Instantiate(pickUps[pickupIndex], transform.position, Quaternion.identity);
        }
    }
}
