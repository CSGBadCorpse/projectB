using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCamera : MonoBehaviour
{


    [SerializeField]
    private CinemachineVirtualCamera bossCamera;


    private void Start()
    {
        bossCamera.Priority = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            bossCamera.Priority = 10;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            bossCamera.Priority = 0;
        }
    }
}
