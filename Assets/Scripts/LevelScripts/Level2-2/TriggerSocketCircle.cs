using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSocketCircle : MonoBehaviour
{
    [SerializeField]
    private GameObject circle;
    private void Start()
    {
        circle.SetActive(false);
    }
    void Update()
    {
        if (PlayerController.Instance.PickUpStick)
        {
            circle.SetActive(true);
        }
    }
}
