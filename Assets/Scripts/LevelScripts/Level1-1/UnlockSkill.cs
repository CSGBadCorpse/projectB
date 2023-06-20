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

    private void Start()
    {
        text.SetActive(false);
        entered = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)&& entered)
        {
            //controller.enabled = true;
            birdMove.ReturnPosition();
            //birdMove.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")&&!PlayerController.Instance.hasChangeSpaceSkill)
        {
            entered = false;
            text.gameObject.SetActive(true);
            PlayerController.Instance.hasChangeSpaceSkill = true;
            PlayerController.Instance.SetAnimatorStop();
            controller.enabled = false;
            birdMove.enabled = true;
            birdMove.returnPos = false;
            birdMove.SetRotateTarget(orientationPoint);
            birdMove.SetRotateType(RotateType.skill_Space);
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
