using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entered : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera puzzleCamera;
    public bool entered { get; private set; }

    private void Start()
    {
        puzzleCamera.Priority = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            entered = true;
            puzzleCamera.Priority = 10;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            entered = false;
            puzzleCamera.Priority = 0;
        }
    }
}
