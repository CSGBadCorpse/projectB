using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //角色控制脚本
    public static PlayerController Instance { private set; get; }
    public event EventHandler Event_ActivateTriggers;
    //

    private Transform trans;
    private Rigidbody rb;
    private Vector3 moveDir;


    [SerializeField]
    [Header("相机控制脚本")]
    private CameraController cameraInstance = CameraController.Instance;

    [Header("角色控制SO")]
    [SerializeField]
    private PlayerMovemenSO playerMovemenSO;
    [Header("动画机")]
    [SerializeField]
    private Animator animator;

    /*[Header("跳跃参数")]
    [SerializeField]*/
    private float jumpForce;
    //[SerializeField]
    private float jumpCooldown;
    //[SerializeField]
    private float airMultiplier;
    private bool readyToJump;

    private float playerHeight;
    private bool isOnGround;
    /*[SerializeField]
    [Header("环境检测")]*/
    //用来判断哪一个是地面的图层
    private LayerMask groundLayer;
    /*[SerializeField]
    [Header("跳跃检测距离")]*/
    private float jumpDistance;

    //[SerializeField]
    private float moveSpeed;
    //[SerializeField]
    //[Header("控制角色移动手感的微调参数")]
    private float offset;
    [SerializeField]
    [Header("玩家本地坐标")]
    private Transform localTransform;
    /*[SerializeField]
    [Header("旋转速度")]*/
    private float rotationSpeed = 2.0f;
    /*[SerializeField]
    [Header("地面摩擦力")]*/
    private float groundDrag;
    private string triggerName;
    //是否拾取了道具
    private bool pickUpStick;

    public bool hasChangeSpaceSkill;
    


    private void Awake()
    {
        Instance = this;
        hasChangeSpaceSkill = false;
    }

    private void Start()
    {
        jumpForce = playerMovemenSO.jumpForce;
        jumpCooldown = playerMovemenSO.jumpCooldown;
        airMultiplier = playerMovemenSO.airMultiplier;
        groundLayer = playerMovemenSO.groundLayer;
        jumpDistance = playerMovemenSO.jumpDistance;
        moveSpeed = playerMovemenSO.moveSpeed;
        offset = playerMovemenSO.offset;
        rotationSpeed = playerMovemenSO.rotationSpeed;
        groundDrag = playerMovemenSO.groundDrag;
        triggerName = playerMovemenSO.triggerName;

        pickUpStick = false;

        trans = transform;
        playerHeight = localTransform.localScale.y;
        rb = GetComponent<Rigidbody>();
        readyToJump = true;

        
    }


    private void Update()
    {
        Debug.DrawRay(localTransform.position-new Vector3(localTransform.position.x, localTransform.position.y- localTransform.localScale.y/2, localTransform.position.z), Vector2.down * (playerHeight * 0.5f + jumpDistance), Color.red);
        bool hit = Physics.Raycast(localTransform.position, localTransform.TransformDirection(Vector3.down), playerHeight * 0.5f + jumpDistance, groundLayer);
        if (hit)
        {
            isOnGround = true;
            animator.SetBool("OnGround",isOnGround);
            rb.drag = groundDrag;
        }
        else
        {
            isOnGround = false;
            animator.SetBool("OnGround", isOnGround);
            rb.drag = 0;
        }
        Movement();//移动
        SpeedControl();

        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateTriggers();//触发互动机关
        }
    }


    private void FixedUpdate()
    {
        if (isOnGround)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!isOnGround)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*Debug.Log(triggerName);*/
        if (other.gameObject.CompareTag(triggerName))
        {
            Destroy(other.gameObject);
            /*Debug.Log(other.name);*/
            //捡起来
            pickUpStick = true;
            if (pickUpStick)
            {
                Debug.Log("捡起了道具");
            }
        }
    }

    private void Movement()//移动
    {
        if (!ViewChange.Instance.IsChangeFinished())
        {
            moveDir = Vector3.zero;
            return;
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && readyToJump)
        {
            //rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z) + Vector3.up * jumpForce;
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);

        }
        //movement
        if (cameraInstance.Is3DGame())
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            if (horizontal >= offset || vertical >= offset || horizontal<=-offset || vertical <= -offset) 
            {
                moveDir = new Vector3(horizontal, 0, vertical);
                localTransform.forward = Vector3.Slerp(localTransform.forward, new Vector3(moveDir.x,0,moveDir.z), rotationSpeed * Time.deltaTime);
            }
            else
            {
                moveDir = Vector3.zero;
            }
            
        }
        else if (cameraInstance.Is2DGame())
        {
            if (trans.position.z < -1.1f|| trans.position.z > 1.1f)
            {
                trans.position = new Vector3(trans.position.x, trans.position.y, -1f);
            }
            float horizontal = Input.GetAxis("Horizontal");
            animator.SetFloat("Speed", Math.Abs(horizontal));
            moveDir = new Vector3(horizontal, 0, 0);
            localTransform.forward = Vector3.Slerp(localTransform.forward, new Vector3(moveDir.x, 0, moveDir.z), rotationSpeed * Time.deltaTime);
        }
        
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(new Vector3(0, 1 * jumpForce, 0), ForceMode.Impulse);
        //animator.SetBool("OnGround", true);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void SpeedControl()//速度控制
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Fix2DPosition()//2d模式下的移动轴和3d模式下的移动轴不在一条轴上
    {
        localTransform.rotation = Quaternion.identity;
        localTransform.forward = localTransform.right;
        trans.position = new Vector3(trans.position.x, trans.position.y, -1);
    }
    public void Fix3DPosition()//因为只需要执行一次，在切换到3D时执行一次
    {
        trans.position = new Vector3(trans.position.x, trans.position.y, 0);
    }

    private void ActivateTriggers()
    {
        Event_ActivateTriggers?.Invoke(this, EventArgs.Empty);
    }

    public bool HasTrigger()
    {
        return pickUpStick;
    }
}
