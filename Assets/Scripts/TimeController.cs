using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeMode
{
    Now,
    Old
}
public class TimeController : MonoBehaviour
{
    [Header("现在时空的物体")]
    [SerializeField]
    private List<Transform> objectsNow;
    [Header("过去时空的物体")]
    [SerializeField]
    private List<Transform> objectsOld;
    [Header("切换时间遮罩")]
    [SerializeField]
    private CanvasGroup effectCanvas;
    [Header("遮罩显示速度")]
    [SerializeField]
    private float appearSpeed;
    [Header("遮罩消失速度")]
    [SerializeField]
    private float disappearSpeed;


    private bool switchTime;
    private TimeMode time;
    private bool countToAppear;
    private bool countToDisappear;
    void Start()
    {
        time = TimeMode.Now;
        switchTime = false;
        countToAppear = false;
        countToDisappear = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        switchTime = Input.GetKeyDown(KeyCode.R);//切换时空技能键
        if (switchTime)
        {
            countToAppear = true;
            /*if (effectCanvas.alpha != 1)
            {
                effectCanvas.alpha += 0.001f;
            }
            if(effectCanvas.alpha >= 1)
            {
                effectCanvas.alpha = 1;
                Debug.Log("Change Time");
                foreach (Transform t in objectsNow)
                {
                    if (time == TimeMode.Now)
                    {
                        t.gameObject.SetActive(false);
                    }
                    else if (time == TimeMode.Old)
                    {
                        t.gameObject.SetActive(true);
                    }
                }
                foreach (Transform t in objectsOld)
                {
                    if (time == TimeMode.Now)
                    {
                        t.gameObject.SetActive(true);
                    }
                    else if (time == TimeMode.Old)
                    {
                        t.gameObject.SetActive(false);
                    }
                }
                if (time == TimeMode.Old)
                {
                    time = TimeMode.Now;
                }
                else if (time == TimeMode.Now)
                {
                    time = TimeMode.Old;
                }
            }*/
            
        }
        if (countToAppear && effectCanvas.alpha != 1)
        {
            effectCanvas.alpha += appearSpeed;
            if(effectCanvas.alpha == 1)
            {
                foreach (Transform t in objectsNow)
                {
                    if (time == TimeMode.Now)
                    {
                        t.gameObject.SetActive(false);
                    }
                    else if (time == TimeMode.Old)
                    {
                        t.gameObject.SetActive(true);
                    }
                }
                foreach (Transform t in objectsOld)
                {
                    if (time == TimeMode.Now)
                    {
                        t.gameObject.SetActive(true);
                    }
                    else if (time == TimeMode.Old)
                    {
                        t.gameObject.SetActive(false);
                    }
                }
                if (time == TimeMode.Old)
                {
                    time = TimeMode.Now;
                }
                else if (time == TimeMode.Now)
                {
                    time = TimeMode.Old;
                }
                countToAppear = false;
                countToDisappear = true;
            }
        }
        if (countToDisappear)
        {
            effectCanvas.alpha -= disappearSpeed;
            if(effectCanvas.alpha == 0)
            {
                countToDisappear = false;
            }
            
        }
    }
}
