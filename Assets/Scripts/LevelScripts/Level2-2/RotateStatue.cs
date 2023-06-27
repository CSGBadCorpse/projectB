using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum TargetRotation
{
    [EnumAttirbute("前")]
    Forward,
    [EnumAttirbute("后")]
    Backward,
    [EnumAttirbute("左")]
    Left,
    [EnumAttirbute("右")]
    Right
}

public class RotateStatue : MonoBehaviour
{

    private bool enableRotate;
    public bool EnableRotate
    {
        get { return enableRotate; }
    }
    //private bool activate;
    [SerializeField]
    public bool activate;

    private bool finalDoorOpened;

    [SerializeField]
    private Transform statue;
    [EnumAttirbute("目标方向")]
    [SerializeField]
    private TargetRotation targetRotation;

    private void Start()
    {
        finalDoorOpened = false;
        activate = false;
        enableRotate = false;
        FinalDoor.Instance.Event_OnDoorOpened += FinalDoor_Event_OnDoorOpened;
    }

    private void FinalDoor_Event_OnDoorOpened(object sender, EventArgs e)
    {
        finalDoorOpened = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            enableRotate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            enableRotate = false;
        }
    }

    private void Update()
    {
        if (enableRotate && TimeController.Instance.SwitchTime)
        {
            enableRotate = false;
        }
        if (enableRotate&&!finalDoorOpened)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                statue.Rotate(0, 90, 0);
            }
        }
        //Debug.Log(this.gameObject.name + " : " + statue.eulerAngles.y);
        if(statue.eulerAngles.y == GetRotationFromEnum(targetRotation))
        {
            activate = true;
        }
        else
        {
            activate = false;
        }
        //activate=statue.eulerAngles.y == GetRotationFromEnum(targetRotation) ? true : false;
    }


    private float GetRotationFromEnum(TargetRotation targetRotation)
    {
        switch (targetRotation)
        {
            case TargetRotation.Forward:
                return 0f;
                //break;
            case TargetRotation.Backward:
                return 180f;
                //break;
            case TargetRotation.Left:
                return 90f;
                //break;
            case TargetRotation.Right:
                return 270f;
                //break;
            default:
                return 0f;
                //break;
        }
    }
    
}
