using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotation : MonoBehaviour
{
    [Header("旋转速度")]
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Transform rotationTarget;
    void Update()
    {
        rotationTarget.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
    }
}
