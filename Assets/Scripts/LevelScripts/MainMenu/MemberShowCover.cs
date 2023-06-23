using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberShowCover : MonoBehaviour
{
    [Header("结束遮罩")]
    [SerializeField]
    private CanvasGroup effectCanvas;
    [Header("遮罩显示速度")]
    [SerializeField]
    private float appearSpeed;
    private bool show;
    private bool hide;
    // Start is called before the first frame update
    void Start()
    {
        show = true;
        hide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (show && effectCanvas.gameObject.active)
        {
            effectCanvas.alpha += appearSpeed;
            if (effectCanvas.alpha == 1)
            {
                show = false;
            }

        }
        if(hide && effectCanvas.gameObject.active)
        {
            effectCanvas.alpha -= appearSpeed;
            if (effectCanvas.alpha == 0)
            {
                hide = false;
                effectCanvas.gameObject.active=false;
                show = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)&&(effectCanvas.alpha == 1|| effectCanvas.alpha == 0))
        {
            hide = true;
        }

    }
}
