using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLeafNow : MonoBehaviour
{
    [SerializeField]
    private float standTime = 5f;
    [Header("对应的旧时空树叶")]
    [SerializeField]
    private GameObject oldLeaf;



    private bool startCountDown;
    private float currentTime;

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
    }

    private void TimeController_Event_OnTimeChanged(object sender, System.EventArgs e)
    {
        //时空切换
        if (TimeController.Instance.IsNow())
        {
            if (oldLeaf.GetComponent<TreeLeafOld>().GetCurrentTimer() > 0)
            {
                currentTime = standTime - oldLeaf.GetComponent<TreeLeafOld>().GetCurrentTimer();
            }
            EnableSelf();
        }
        else if (!TimeController.Instance.IsNow())
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
        if(currentTime >= standTime)
        {
            currentTime = standTime;
            DisableSelf();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            startCountDown = true;
        }
    }

    private void ResetLevel()
    {
        currentTime = 0;
        startCountDown = false;
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
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


    public float GetStandTime()
    {
        return standTime;
    }

    public float GetCurrentTimer()
    {
        return currentTime;
    }
}
