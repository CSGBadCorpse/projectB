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

    private void Start()
    {
        text.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
            text.gameObject.SetActive(true);
            PlayerController.Instance.hasChangeSpaceSkill = true;
            PlayerController.Instance.SetAnimatorStop();
            controller.enabled = false;
            birdMove.enabled = true;
            birdMove.SetRotateTarget(orientationPoint);
            birdMove.SetRotateType(RotateType.skill);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
        }
    }
}
