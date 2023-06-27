using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraControl : MonoBehaviour
{
    public static VirtualCameraControl Instance;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private CinemachineVirtualCamera mainVirtualCam;
    public float fallIndex;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Respawn.Instance.OnPlayerRespawn += Respawn_OnPlayerRespawn;
    }

    private void Respawn_OnPlayerRespawn(object sender, System.EventArgs e)
    {
        mainVirtualCam.m_Follow = player;
        mainVirtualCam.m_LookAt = player;
        //throw new System.NotImplementedException();
        //StartCoroutine(FollowPlayer());
    }
    IEnumerator FollowPlayer()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Activated");
        mainVirtualCam.m_Follow = player;
        mainVirtualCam.m_LookAt = player;
    }

    private void Update()
    {
        if (mainVirtualCam.gameObject.transform.position.y< fallIndex) 
        {
            //mainVirtualCam.gameObject.transform.position = new Vector3(mainVirtualCam.gameObject.transform.position.x, 4.1f, mainVirtualCam.gameObject.transform.position.z);
            mainVirtualCam.m_Follow = null;
            mainVirtualCam.m_LookAt = null;
        }
    }
}
