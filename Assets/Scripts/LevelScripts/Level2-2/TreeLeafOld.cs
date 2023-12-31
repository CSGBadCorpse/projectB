using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreeLeafOld : MonoBehaviour
{
    [SerializeField]
    private float standTime = 3f;
    public float StandTime
    {
        get { return standTime; }
    }

    [Header("对应的现时空树叶")]
    [SerializeField]
    private GameObject nowLeaf;

    private bool startCountDown;
    private float currentTime;

    private bool touchedLeaf;

    public float CurrentTime
    {
        get { return currentTime; }
    }

    [SerializeField]
    private GameObject enabledLeafObject;
    [SerializeField]
    private GameObject disabledLeafObject;
    [SerializeField]
    private GameObject disableStickObject;
    [SerializeField]
    private MeshRenderer meshRenderer;


    //private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;


    private void Start()
    {
        TimeController.Instance.Event_OnTimeChanged += TimeController_Event_OnTimeChanged;
        startCountDown = false;
        currentTime = 0;
        //meshRenderer = this.GetComponent<MeshRenderer>();
        boxCollider = this.GetComponent<BoxCollider>();
        Respawn.Instance.OnPlayerRespawn += Respawn_OnPlayerRespawn;
        DisableSelf();
        touchedLeaf = false;
    }

    private void TimeController_Event_OnTimeChanged(object sender, System.EventArgs e)
    {
        //时空切换
        if (TimeController.Instance.IsNow())//现在时空
        {
            startCountDown = false;
            DisableSelf();
        }
        else if (!TimeController.Instance.IsNow())//旧时空
        {
            //startCountDown = false;
            float nowLeafStandCurrentTime = nowLeaf.GetComponent<TreeLeafNow>().CurrentTime;
            if (nowLeafStandCurrentTime > 0 && nowLeafStandCurrentTime >= standTime && currentTime != standTime)
            {
                currentTime = standTime - (nowLeaf.GetComponent<TreeLeafNow>().StandTime - nowLeaf.GetComponent<TreeLeafNow>().CurrentTime);
            }
            else if (nowLeafStandCurrentTime > 0 && nowLeafStandCurrentTime < standTime && currentTime != standTime)
            {
                currentTime =  nowLeaf.GetComponent<TreeLeafNow>().CurrentTime;
            }
            EnableSelf();
        }
    }

    private void Respawn_OnPlayerRespawn(object sender, System.EventArgs e)
    {
        ResetLevel();
    }

    private void Update()
    {
        if (touchedLeaf && PlayerController.Instance.IsOnGround)
        {
            startCountDown = true;
        }
        if (TimeController.Instance.IsNow())
        {
            startCountDown = false;
            DisableSelf();
        }
        else if (!TimeController.Instance.IsNow())
        {
            EnableSelf();
        }
        if (startCountDown && currentTime < standTime)
        {
            currentTime += Time.deltaTime;
        }
        if (currentTime >= standTime)
        {
            currentTime = standTime;
            DisableSelf();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            touchedLeaf = true;
        }
    }

    private void ResetLevel()
    {
        currentTime = 0;
        startCountDown = false;
        touchedLeaf = false;
        EnableSelf();
    }





    private void DisableSelf()
    {
        meshRenderer.enabled = false;
        //enabledLeafObject.SetActive(false);
        if (!TimeController.Instance.IsNow())
        {
            //disabledLeafObject.SetActive(true);
            disableStickObject.SetActive(true);
        }
        else if (TimeController.Instance.IsNow())
        {
            //disabledLeafObject.SetActive(false);
            disableStickObject.SetActive(false);
        }
        boxCollider.enabled = false;
    }

    private void EnableSelf()
    {
        meshRenderer.enabled = true;
        //enabledLeafObject.SetActive(true);
        //disabledLeafObject.SetActive(false);
        disableStickObject.SetActive(true);
        boxCollider.enabled = true;
    }

    /* public float GetStandTime()
     {
         return standTime;
     }

     public float GetCurrentTimer()
     {
         return currentTime;
     }*/
    private GUIStyle _tabStyle;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        _tabStyle = new GUIStyle();
        _tabStyle.alignment = TextAnchor.MiddleLeft;
        _tabStyle.fontSize = 16;
        _tabStyle.normal.textColor = Color.blue;
        Handles.Label(this.transform.position, "TreeOld: " + standTime + "/" + currentTime.ToString("f1"), _tabStyle);
    }
#endif

}
