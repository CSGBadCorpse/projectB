using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public static FinalDoor Instance { get; private set; }
    public event EventHandler Event_OnDoorOpened;

    [SerializeField]
    private GameObject firstStatue;
    [SerializeField]
    private GameObject secondStatue;
    [SerializeField]
    private GameObject thirdStatue;
    [SerializeField]
    private GameObject door;

    private bool firstActivated;
    private bool secondActivated;
    private bool thirdActivated;

    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        
    }

    private void Update()
    {
        firstActivated = firstStatue.GetComponent<RotateStatue>().GetActivatedState();
        secondActivated = secondStatue.GetComponent<RotateStatue>().GetActivatedState();
        thirdActivated = thirdStatue.GetComponent<RotateStatue>().GetActivatedState();
        if (firstActivated && secondActivated && thirdActivated)
        {
            Event_OnDoorOpened?.Invoke(this, EventArgs.Empty);
            Debug.Log("开门动画");
            door.SetActive(false);
            firstActivated = false;
            secondActivated = false;
            thirdActivated = false;
        }
    }
}
