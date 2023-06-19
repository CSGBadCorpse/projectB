using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TreeLeafNow : MonoBehaviour
{
    [SerializeField]
    private float standTime = 5f;
    public float StandTime
    {
        get { return standTime; }
    }
    [Header("对应的旧时空树叶")]
    [SerializeField]
    private GameObject oldLeaf;



    private bool startCountDown;
    private float currentTime;

    private bool touchedLeaf;

    public float CurrentTime
    {
        get { return currentTime; }

    }

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;


    private void Start()
    {
        TimeController.Instance.Event_OnTimeChanged += TimeController_Event_OnTimeChanged;
        startCountDown = false;
        currentTime = 0;
        meshRenderer = this.GetComponent<MeshRenderer>();
        boxCollider = this.GetComponent<BoxCollider>();
        Respawn.Instance.OnPlayerRespawn += Respawn_OnPlayerRespawn;
        EnableSelf();
        touchedLeaf = false;
    }

    private void TimeController_Event_OnTimeChanged(object sender, System.EventArgs e)
    {
        //时空切换
        if (TimeController.Instance.IsNow())//现在时空
        {
            if (oldLeaf.GetComponent<TreeLeafOld>().CurrentTime > 0 && currentTime != standTime)
            {
                currentTime = oldLeaf.GetComponent<TreeLeafOld>().CurrentTime;
            }
            EnableSelf();
        }
        else if (!TimeController.Instance.IsNow())//旧时空
        {
            startCountDown = false;
            DisableSelf();
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
            EnableSelf();
        }
        else if (!TimeController.Instance.IsNow())
        {
            startCountDown = false;
            DisableSelf();
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
        EnableSelf();
        touchedLeaf = false;
    }
    private void DisableSelf()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    private void EnableSelf()
    {
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }

    private GUIStyle _tabStyle;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        _tabStyle = new GUIStyle();
        _tabStyle.alignment = TextAnchor.MiddleLeft;
        _tabStyle.fontSize = 16;
        _tabStyle.normal.textColor = Color.red;
        Handles.Label(this.transform.position - new Vector3(0, 1, 0), "TreeNow: " + standTime + "/" + currentTime.ToString("f1"), _tabStyle);
    }
#endif
}
