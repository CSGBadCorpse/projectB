using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2DRoad : MonoBehaviour
{
    private Collider[] colliders;


    private void Start()
    {
        colliders = this.GetComponents<BoxCollider>();
        if (CameraController.Instance.Is3DGame())
        {
            foreach (var collider in colliders)
            {
                collider.isTrigger = true;
            }
        }
        else if (CameraController.Instance.Is2DGame())
        {
            foreach (var collider in colliders)
            {
                collider.isTrigger = false;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (CameraController.Instance.Is3DGame())
        {
            foreach (var collider in colliders)
            {
                collider.isTrigger = true;
            }
        }
        else if (CameraController.Instance.Is2DGame())
        {
            foreach (var collider in colliders)
            {
                collider.isTrigger = false;
            }
        }
    }
}
