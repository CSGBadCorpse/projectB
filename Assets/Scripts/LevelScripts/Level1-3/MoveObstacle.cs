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
    [SerializeField]
    private GameObject player;
    [SerializeField]
    [Header("相机控制脚本")]
    private CameraController cameraInstance = CameraController.Instance;
    [SerializeField]
    private Transform squareIn2D;
    [SerializeField]
    private MeshRenderer meshRenderer;

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
        if (cameraInstance.Is3DGame())
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            this.GetComponent<BoxCollider>().isTrigger = true;
            squareIn2D.gameObject.SetActive(true);
            meshRenderer.enabled = false;
        }
        if (cameraInstance.Is2DGame() && ViewChange.Instance.IsChangeFinished())
        {
            this.gameObject.layer = LayerMask.NameToLayer("Ground");
            this.GetComponent<BoxCollider>().isTrigger = false;
            squareIn2D.gameObject.SetActive(false);
            meshRenderer.enabled = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Landed");
            //this.gameObject.transform.child
            player.transform.SetParent(this.transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Jump");
            player.transform.SetParent(null);
        }
    }
}
