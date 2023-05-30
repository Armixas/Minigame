using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneManager : MonoBehaviour
{
    private GameObject left;
    private GameObject right;

    private Button leftButton; 
    private Button rightButton;

    private countDown count;
    private upAndDown poleUpDown;
    private Roatation baseRotate;
    private Rotator MainRotor;

    public bool CanMove = false;

    [SerializeField] private GameObject ScoreCanvas;
    [SerializeField] private GameObject CountDown;
    [SerializeField] private GameObject Count;

    [SerializeField] private GameObject pole;
    [SerializeField] private GameObject rotor;
    [SerializeField] private GameObject baseRotation;
    // Start is called before the first frame update
    void Start()
    {
        right = GameObject.Find("Ready");
        left = GameObject.Find("Ready1");

        leftButton = left.GetComponent<Button>();
        rightButton = right.GetComponent<Button>();

        count = Count.GetComponent<countDown>();

        poleUpDown = pole.GetComponent<upAndDown>();
        baseRotate = baseRotation.GetComponent<Roatation>();
        MainRotor = rotor.GetComponent<Rotator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftButton.interactable == false && rightButton.interactable == false)
        {
            ScoreCanvas.SetActive(false);
            CountDown.SetActive(true);
        }
        if (count.lastChange == true)
        {
            CountDown.SetActive(false);
            poleUpDown.enabled = true;
            baseRotate.enabled = true;
            MainRotor.enabled = true;
            CanMove = true;
        }
    }
}
