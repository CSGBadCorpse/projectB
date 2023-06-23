using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockSkill : MonoBehaviour
{

    [SerializeField]
    private GameObject text;

    [SerializeField]
    private Transform orientationPoint;

    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private RotationBird birdMove;
    private bool entered;

    [SerializeField]
    private RotateType rotationType;

    private void Start()
    {
        text.SetActive(false);
        entered = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)&& entered)
        {
            controller.enabled = true;
            birdMove.ReturnPosition();
            //birdMove.enabled = false;
        }
        else if(Input.GetKeyDown(KeyCode.R) && entered)
        {
            controller.enabled = true;
            birdMove.ReturnPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if(rotationType==RotateType.skill_Space && !PlayerController.Instance.skill_ChangeSpace)
            {
                entered = true;
                text.gameObject.SetActive(true);
                PlayerController.Instance.skill_ChangeSpace = true;
                PlayerController.Instance.SetAnimatorStop();
                controller.enabled = false;
                birdMove.enabled = true;
                birdMove.returnPos = false;
                birdMove.SetRotateTarget(orientationPoint);
                birdMove.SetRotateType(rotationType);
            } 
            else if(rotationType == RotateType.skill_Time && !PlayerController.Instance.skill_ChangeTime) 
            {
                entered = true;
                text.gameObject.SetActive(true);
                PlayerController.Instance.skill_ChangeTime = true;
                PlayerController.Instance.SetAnimatorStop();
                controller.enabled = false;
                birdMove.enabled = true;
                birdMove.returnPos = false;
                birdMove.SetRotateTarget(orientationPoint);
                birdMove.SetRotateType(rotationType);
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
            entered = false;
                
        }
    }
}
