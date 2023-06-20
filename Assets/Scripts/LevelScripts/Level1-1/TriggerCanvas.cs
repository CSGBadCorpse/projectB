using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject text;

    [SerializeField]
    private Transform orientationPoint;

    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private RotationBird birdMove;

    [SerializeField]
    private Trigger trigger;

    private bool entered;
    private void Start()
    {
        entered = false;
        text.SetActive(false);
    }
    private void Update()
    {
        if (trigger.IsRotating&& entered)
        {
            entered = false;
            text.gameObject.SetActive(false);
            controller.TriggerEnable();
            birdMove.transform.SetParent(controller.transform);
            birdMove.ReturnPosition();
            //birdMove.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            entered = true;
            text.gameObject.SetActive(true);
            //PlayerController.Instance.hasChangeSpaceSkill = true;
            //PlayerController.Instance.SetAnimatorStop();
            controller.TriggerDisable();
            birdMove.enabled = true;
            birdMove.returnPos = false;
            birdMove.gameObject.transform.SetParent(null);
            birdMove.SetRotateTarget(orientationPoint);
            birdMove.SetRotateType(RotateType.trigger);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
