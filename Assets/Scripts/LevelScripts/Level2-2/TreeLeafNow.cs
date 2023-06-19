using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using UnityEditor;
=======
>>>>>>> c453743883466631bae7c117d56be9f463e542ea

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
        EnableSelf();
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
        {
            if (oldLeaf.GetComponent<TreeLeafOld>().CurrentTime > 0 && currentTime != standTime)
            {
                currentTime = oldLeaf.GetComponent<TreeLeafOld>().CurrentTime;
            }
            EnableSelf();
        }
        else if (!TimeController.Instance.IsNow())//旧时空
=======
        if (TimeController.Instance.IsNow())
        {
            if (oldLeaf.GetComponent<TreeLeafOld>().CurrentTime > 0)
            {
                currentTime = standTime - oldLeaf.GetComponent<TreeLeafOld>().CurrentTime;
            }
            EnableSelf();
        }
        else if (!TimeController.Instance.IsNow())
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
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
<<<<<<< HEAD
        if (touchedLeaf && PlayerController.Instance.IsOnGround)
        {
            startCountDown = true;
        }
=======

>>>>>>> c453743883466631bae7c117d56be9f463e542ea
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
<<<<<<< HEAD
        if (currentTime >= standTime)
=======
        if(currentTime >= standTime)
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
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
        EnableSelf();
        touchedLeaf = false;
=======
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
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
=======

    public float GetStandTime()
    {
        return standTime;
    }
>>>>>>> c453743883466631bae7c117d56be9f463e542ea
}
