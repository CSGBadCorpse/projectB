using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShowCover : MonoBehaviour
{
    [Header("结束遮罩")]
    [SerializeField]
    private CanvasGroup effectCanvas;
    [Header("遮罩显示速度")]
    [SerializeField]
    private float appearSpeed;
    private bool start;
    // Start is called before the first frame update
    void Start()
    {
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(start&& effectCanvas.gameObject.active)
        {
            effectCanvas.alpha += appearSpeed;
            if (effectCanvas.alpha == 1)
            {
                start = false;
            }
            
        }
        
    }
}
