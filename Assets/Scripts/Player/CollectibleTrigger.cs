using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTrigger : MonoBehaviour
{
    [SerializeField] private string fireUP = "FireUP";
    [SerializeField] private string bombUP = "BombUP";
    [SerializeField] private string deathUP = "DeathUP";
    [SerializeField] private string bootUP = "BootUP";
    
    private BombermanPlayerController _player;
    
    private void Awake()
    {
        _player = GetComponent<BombermanPlayerController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        var otherGO = other.gameObject;
        var collected = false;

        if (IsFireUp(otherGO))
            _player.AddExtendedRange();
        else if (IsBombUp(otherGO))
            _player.AddBombCount();
        else if (IsDeathUp(otherGO))
            _player.KillPlayer();
        else if (IsBootUp(otherGO))
            _player.AddBoot();
        else
            return;
        
        // Deletes collectible // Unity lies, collected is used.
        collected = true;
        otherGO.SetActive(false);
        Destroy(otherGO);
    }
    private bool IsFireUp(GameObject obj) => obj.CompareTag(fireUP);
    private bool IsBombUp(GameObject obj) => obj.CompareTag(bombUP);
    private bool IsDeathUp(GameObject obj) => obj.CompareTag(deathUP);
    private bool IsBootUp(GameObject obj) => obj.CompareTag(bootUP);

}
