using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlankTriggerCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject text;

    [SerializeField]
    private Transform orientationPoint;

    [SerializeField]
    private PlayerController controller;
    [SerializeField]
    private RotationBird birdMove;

    /*[SerializeField]
    private Trigger trigger;*/

    private bool pressed;
    [SerializeField]
    private TriggerSocket triggerSocket;
    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        pressed = false;
        PlayerController.Instance.Event_ActivateTriggerCanvas += Instance_Event_ActivateTriggerCanvas;
    }

    private void Instance_Event_ActivateTriggerCanvas(object sender, System.EventArgs e)
    {
        //show = true;
        text.SetActive(true);
        controller.TriggerDisable();
        birdMove.enabled = true;
        birdMove.returnPos = false;
        birdMove.gameObject.transform.SetParent(null);
        birdMove.SetRotateTarget(orientationPoint);
        birdMove.SetRotateType(RotateType.trigger);
        birdMove.isPlank = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && triggerSocket.ApproachTrigger && controller.PickUpStick)
        {
            pressed = true;
        }

        if (pressed && controller.PickUpStick)
        {
            pressed = false;
            text.gameObject.SetActive(false);
            controller.TriggerEnable();
            birdMove.transform.SetParent(controller.transform);
            birdMove.ReturnPosition();
            
        }
    }
}
