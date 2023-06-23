using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSocket : MonoBehaviour
{
    public static TriggerSocket Instance { get; private set; }
    public event EventHandler Event_ActivateMovingBlock;


    private bool approachTrigger;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        approachTrigger = false;
        PlayerController.Instance.Event_ActivateTriggers += Player_Event_ActivateTriggers;
    }

    private void Player_Event_ActivateTriggers(object sender, System.EventArgs e)
    {
        if (approachTrigger)
        {
            if (PlayerController.Instance.HasTrigger() && !TimeController.Instance.IsNow())
            {
                Debug.Log("Active Moving plank");
                Event_ActivateMovingBlock?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))//检测是否为玩家
        {
            approachTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            approachTrigger = false;
        }
    }
}
