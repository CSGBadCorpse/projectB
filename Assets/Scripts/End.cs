using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class End : MonoBehaviour
{
    [SerializeField]
    private BoxCollider ShowEnd;
    /*[SerializeField]
    private GameObject text;*/
    [Header("结束遮罩")]
    [SerializeField]
    private CanvasGroup effectCanvas;
    [Header("遮罩显示速度")]
    [SerializeField]
    private float appearSpeed;
    private bool end;
    [SerializeField]
    private CinemachineVirtualCamera endCamera;

    // Start is called before the first frame update
    void Start()
    {
        //text.SetActive(false);
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (end && effectCanvas.alpha != 1)
        {
            effectCanvas.alpha += appearSpeed;
        }
        if(effectCanvas.alpha == 1)
        {
            ChangeLevel.Instances.GotoEndScene();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //text.gameObject.SetActive(true);
            end = true;
            endCamera.Priority = 10;
        }
    }
}
