using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //角色控制脚本
    public static PlayerController Instance { private set; get; }

    private Transform trans;
    private Rigidbody rb;
    private Vector3 moveDir;
    [SerializeField]
    private float moveSpeed = 5f;


    public CameraController cameraInstance = CameraController.Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        trans = transform;
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        Movement();//移动
    }

    private void FixedUpdate()
    {
        rb.MovePosition(trans.position + moveDir * moveSpeed * Time.deltaTime);//实际移动语句
    }

    private void Movement()//移动
    {
        if (!ViewChange.Instance.IsChangeFinished())
        {
            moveDir = Vector3.zero;
            return;
        }
        if (cameraInstance.Is3DGame())
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float offset = 0.3f;
            if (horizontal >= offset || vertical >= offset || horizontal<=-offset || vertical <= -offset) 
            {
                moveDir = new Vector3(horizontal, 0, vertical);
                //moveDir = transform.TransformDirection(moveDir);//将moveDir从本地坐标系转换为世界坐标系
                moveDir = Quaternion.Euler(0, cameraInstance.GetVirtualCamera().transform.eulerAngles.y, 0) * moveDir;//让角色移动方向沿着相机视角方向
                moveDir.y = 0;
                moveDir = moveDir.normalized;

                Debug.Log(moveDir);
            }
            else
            {
                moveDir = Vector3.zero;
            }
            
        }
        else if (cameraInstance.Is2DGame())
        {

            float horizontal = Input.GetAxis("Horizontal");
            //float vertical = Input.GetAxis("Vertical");
            moveDir = new Vector3(horizontal, 0, 0);
            Fix2DPosition();//修正不同轴的位置
        }
        
    }

    private void Fix2DPosition()//2d模式下的移动轴和3d模式下的移动轴不在一条轴上
    {
        //ViewChange.Instance.IsChangeFinished() && 
        if (cameraInstance.IsOrthographic())
        {
            trans.position = new Vector3(trans.position.x, trans.position.y, -1);
        }
    }
    public void Fix3DPosition()//因为只需要执行一次，在切换到3D时执行一次
    {
        trans.position = new Vector3(trans.position.x, trans.position.y, 0);
        //test commit

    }
}
