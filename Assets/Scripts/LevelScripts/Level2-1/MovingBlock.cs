using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MovingBlock : MonoBehaviour
{
    [SerializeField]
    [Header("移动速度")]
    float moveSpeed;

    [SerializeField]
    [Header("移动路径")]
    Transform[] routes;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private AudioSource chainAudio;
    
    int index = 0;

    public bool activated;//机关是否启用
    // Start is called before the first frame update
    void Start()
    {
        chainAudio.enabled = false;
        activated = false;
        TriggerSocket.Instance.Event_ActivateMovingBlock += TriggerSocket_Event_ActivateMovingBlock;
    }

    private void TriggerSocket_Event_ActivateMovingBlock(object sender, System.EventArgs e)
    {
        activated = !activated;
        chainAudio.enabled = !chainAudio.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == routes[index].position)
        {
            if(index < 5)
            {
                index++;
            }else if (index == 5)
            {
                index = 0;
            }
        }
        else
        { 
            float step = moveSpeed * Time.deltaTime;
            if (activated)
            {
               
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, routes[index].position, step);
            }
            
        }
    }

    public void SetTriggerActivated()
    {
        activated=true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Debug.Log("Landed");
            //this.gameObject.transform.child
            player.transform.SetParent(this.transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Debug.Log("Jump");
            player.transform.SetParent(null);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        for(int i = 0; i < routes.Length-1; i++)
        {
            //Debug.DrawLine(routes[i].position, routes[i + 1].position, Color.red);
            GizmoDrawLine(routes[i].position, routes[i + 1].position, Color.red, "移动路线", Color.blue);

        }
        GizmoDrawLine(routes[routes.Length - 1].position, routes[0].position, Color.red, "移动路线", Color.blue);
    }
    private void GizmoDrawLine(Vector3 from, Vector3 to, Color lineColor, string text, Color textColor)
    {
        Handles.color = lineColor;
        Handles.DrawAAPolyLine(5f, from, to);
        Vector3 dir = (to - from).normalized;
        float distance = Vector3.Distance(from, to);
        for(float i = 0; i < distance; i += 1f)
        {
            Handles.DrawAAPolyLine(
                5f,
                from + dir * i,
                from + (dir * (i - .18f)) + Quaternion.AngleAxis(Time.realtimeSinceStartup * 360f, dir.normalized * 300f) * Vector3.up * .2f

            );
            Handles.DrawAAPolyLine(
                5f,
                from + dir * i,
                from + (dir * (i - .18f)) + Quaternion.AngleAxis(Time.realtimeSinceStartup * 360f + 180, dir.normalized * 300f) * Vector3.up * .2f

            );
        }
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = textColor;
        Handles.Label(from + (dir * distance * .5f), text, style);
    }
#endif
}
