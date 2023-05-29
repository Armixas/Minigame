using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnSpawn : MonoBehaviour
{
    [SerializeField] private GameObject Zombie;
    [SerializeField] private GameObject Tiger;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        InputActionMap actionMap = playerInput.currentActionMap;

        if (string.Join("\n", actionMap.FindAction("Up").controls).Contains("Left"))
        {
            //player1
            GameObject model = Instantiate(Zombie);
            model.transform.parent = transform;
            transform.position = new Vector3(-2, 5, -3);
        }
        else
        {
            //player2
            GameObject model = Instantiate(Tiger);
            model.transform.parent = transform;
            transform.position = new Vector3(-2, 5, -3);
        }

    }
}
