using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using UnityEditor;
=======
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
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

<<<<<<< HEAD
    private bool touchedLeaf;

=======
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
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
        DisableSelf();
<<<<<<< HEAD
        touchedLeaf = false;
=======
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
    }

    private void TimeController_Event_OnTimeChanged(object sender, System.EventArgs e)
    {
        //时空切换
<<<<<<< HEAD
        if (TimeController.Instance.IsNow())//现在时空
=======
        if (TimeController.Instance.IsNow())
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
        {
            startCountDown = false;
            DisableSelf();
        }
<<<<<<< HEAD
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
                currentTime = standTime - nowLeaf.GetComponent<TreeLeafNow>().CurrentTime;
            }
=======
        else if (!TimeController.Instance.IsNow())
        {
            //startCountDown = false;
            if (nowLeaf.GetComponent<TreeLeafNow>().CurrentTime > 0)
            {
                currentTime = standTime - (nowLeaf.GetComponent<TreeLeafNow>().StandTime - nowLeaf.GetComponent<TreeLeafNow>().CurrentTime);
            }
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
            EnableSelf();
        }
    }

    private void Respawn_OnPlayerRespawn(object sender, System.EventArgs e)
    {
        ResetLevel();
    }

    private void Update()
    {
<<<<<<< HEAD
        if (touchedLeaf && PlayerController.Instance.IsOnGround)
        {
            startCountDown = true;
        }
=======
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
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
<<<<<<< HEAD
            touchedLeaf = true;
=======
            startCountDown = true;
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
        }
    }

    private void ResetLevel()
    {
        currentTime = 0;
        startCountDown = false;
<<<<<<< HEAD
        touchedLeaf = false;
=======
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
        EnableSelf();
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

<<<<<<< HEAD
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
=======
    public float GetStandTime()
    {
        return standTime;
    }

    public float GetCurrentTimer()
    {
        return currentTime;
    }
>>>>>>> c453743883466631bae7c117d56be9f463e542ea

}
