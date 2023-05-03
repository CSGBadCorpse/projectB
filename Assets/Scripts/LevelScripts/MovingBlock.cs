using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField]
    [Header("移动速度")]
    float moveSpeed;

    [SerializeField]
    [Header("移动路径")]
    Transform[] routes;
    int index = 0;

    public bool activated;//机关是否启用
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        TriggerSocket.Instance.Event_ActivateMovingBlock += TriggerSocket_Event_ActivateMovingBlock;
    }

    private void TriggerSocket_Event_ActivateMovingBlock(object sender, System.EventArgs e)
    {
        activated = !activated;
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
}
