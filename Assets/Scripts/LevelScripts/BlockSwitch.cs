using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitch : MonoBehaviour
{
    //切换碰撞体状态的本，2d启用，3d禁用
    private BoxCollider boxCollider;
    [SerializeField]
    private Transform viewOf2D;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (CameraController.Instance.Is2DGame())
        {
            boxCollider.isTrigger = false;
            if (viewOf2D != null)
            {
                viewOf2D.gameObject.SetActive(true);
            }
        }
        else if (CameraController.Instance.Is3DGame())
        {
            boxCollider.isTrigger = true;
            if (viewOf2D != null)
            {
                viewOf2D.gameObject.SetActive(false);
            }
        }
    }
}
