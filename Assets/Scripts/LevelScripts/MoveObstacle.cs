using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    [SerializeField]
    private float moveCircleTime;
    private float currentTime;
    [SerializeField]
    private float moveInterval;

    private void Start()
    {
        currentTime = moveCircleTime;
    }
    void Update()
    {
        if(currentTime > moveCircleTime * 0.5f)
        {
            currentTime -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x + moveInterval, transform.position.y, transform.position.z);
        }
        if(currentTime > 0 && currentTime <= moveCircleTime * 0.5f) 
        {
            currentTime -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x - moveInterval, transform.position.y, transform.position.z);
        }
        if(currentTime < 0)
        {
            currentTime = moveCircleTime;
        }
        
    }
}
