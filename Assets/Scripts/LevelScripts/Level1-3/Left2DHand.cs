using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left2DHand : MonoBehaviour
{
    private BoxCollider boxCollider;
    [SerializeField]
    private Transform followPos;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraController.Instance.Is3DGame())
        {
            Deactivate2DHit();
        }
        transform.position = new Vector3(followPos.position.x, transform.position.y, transform.position.z);
    }
    public void Activate2DHit()
    {
        Debug.Log("Activate");
        if (CameraController.Instance.Is2DGame())
        {
            boxCollider.isTrigger = true;
            boxCollider.enabled = true;
        }
    }
    public void Deactivate2DHit()
    {
        Debug.Log("Deactivate");
        boxCollider.isTrigger = true;
        boxCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Respawn.Instance.ResetLevel();
        }
    }
}
