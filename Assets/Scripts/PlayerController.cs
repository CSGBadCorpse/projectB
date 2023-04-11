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
    [Header("相机控制脚本")]
    private CameraController cameraInstance = CameraController.Instance;


    [Header("跳跃参数")]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpCooldown;
    [SerializeField]
    private float airMultiplier;
    private bool readyToJump;

    private float playerHeight;
    private bool isOnGround;
    [SerializeField]
    [Header("环境检测")]
    //用来判断哪一个是地面的图层
    private LayerMask groundLayer;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    [Header("控制角色移动手感的微调参数")]
    private float offset = 0.3f;
    [SerializeField]
    [Header("玩家本地坐标")]
    private Transform localTransform;
    [SerializeField]
    [Header("旋转速度")]
    private float rotationSpeed = 2.0f;
    [SerializeField]
    [Header("地面摩擦力")]
    private float groundDrag;


    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        trans = transform;
        playerHeight = localTransform.localScale.y;
        rb = GetComponent<Rigidbody>();
        readyToJump = true;
        //rb.freezeRotation = true;
        //boxCollider = localTransform.gameObject.GetComponent<BoxCollider>();
    }


    private void Update()
    {
        Debug.DrawRay(localTransform.position, Vector2.down * (playerHeight * 0.5f + 0.1f), Color.red);
        bool hit = Physics.Raycast(localTransform.position, localTransform.TransformDirection(Vector3.down), playerHeight * 0.5f + 0.1f, groundLayer);
        if (hit)
        {
            isOnGround = true;
            rb.drag = groundDrag;
        }
        else
        {
            isOnGround = false;
            rb.drag = 0;
        }
        Movement();//移动
        SpeedControl();
        //Debug.Log(rb.velocity);
    }


    private void FixedUpdate()
    {
        //rb.MovePosition(trans.position + moveDir * moveSpeed * Time.deltaTime);//实际移动语句
        //rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z) + moveDir * moveSpeed * Time.deltaTime;
        if (isOnGround)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!isOnGround)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        
    }

    private void Movement()//移动
    {
        if (!ViewChange.Instance.IsChangeFinished())
        {
            moveDir = Vector3.zero;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && readyToJump)
        {
            //rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z) + Vector3.up * jumpForce;
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);

        }
        if (cameraInstance.Is3DGame())
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            if (horizontal >= offset || vertical >= offset || horizontal<=-offset || vertical <= -offset) 
            {
                moveDir = new Vector3(horizontal, 0, vertical);
                //moveDir = transform.TransformDirection(moveDir);//将moveDir从本地坐标系转换为世界坐标系
                //moveDir = Quaternion.Euler(0, cameraInstance.GetVirtualCamera().transform.eulerAngles.y, 0) * moveDir;//让角色移动方向沿着相机视角方向
                //moveDir.y = trans.position.y;//关键
                localTransform.forward = Vector3.Slerp(localTransform.forward, new Vector3(moveDir.x,0,moveDir.z), rotationSpeed * Time.deltaTime);

                //Debug.Log(moveDir);
            }
            else
            {
                moveDir = Vector3.zero;
            }
            
        }
        else if (cameraInstance.Is2DGame())
        {
            //localTransform.rotation = Quaternion.identity;
            //localTransform.forward = localTransform.right;
            if (trans.position.z < -1.1f|| trans.position.z > 1.1f)
            {
                trans.position = new Vector3(trans.position.x, trans.position.y, -1f);
            }
            float horizontal = Input.GetAxis("Horizontal");
            //float vertical = Input.GetAxis("Vertical");
            moveDir = new Vector3(horizontal, 0, 0);
            localTransform.forward = Vector3.Slerp(localTransform.forward, new Vector3(moveDir.x, 0, moveDir.z), rotationSpeed * Time.deltaTime);
            //Debug.Log(moveDir);
            //Fix2DPosition();//修正不同轴的位置
        }
        
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void SpeedControl()//速度控制
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.y);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Fix2DPosition()//2d模式下的移动轴和3d模式下的移动轴不在一条轴上
    {
        //ViewChange.Instance.IsChangeFinished() && 
        /*if (cameraInstance.IsOrthographic())
        {
            
        }*/
        localTransform.rotation = Quaternion.identity;
        localTransform.forward = localTransform.right;
        trans.position = new Vector3(trans.position.x, trans.position.y, -1);
    }
    public void Fix3DPosition()//因为只需要执行一次，在切换到3D时执行一次
    {
        trans.position = new Vector3(trans.position.x, trans.position.y, 0);
        //test commit

    }

}
