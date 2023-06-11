using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject objectA; // 游戏物体A
    public GameObject objectB; // 游戏物体B

    public GameObject objectC;
    public GameObject objectD;

    public float rotationSpeed; // 旋转速度
    public float rotationSpeed1; // 旋转速度1

    private bool isRotating; // 是否正在旋转
    private Vector3 rotationAxis = Vector3.forward; // 旋转轴向
    private float rotationAngle = 180f; // 旋转角度
    private float rotationAngle1 = 90f; // 旋转角度

    [SerializeField]
    [Header("2D操作出发区域")]
    private Entered enterArea;

    private bool enter;
    private bool area2DEnter;

    private void Start()
    {
        enter = false;
        area2DEnter = false;
        objectC.transform.RotateAround(objectD.transform.position, rotationAxis, 180f);
        enterArea.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (enterArea.entered)
        {
            area2DEnter = true;
        }
        if (CameraController.Instance.Is2DGame())
        {
            enterArea.gameObject.SetActive(true);
        }
        else if (CameraController.Instance.Is3DGame())
        {
            enterArea.gameObject.SetActive(false);
        }
        // 按下f键时开始旋转
        if (Input.GetKeyDown(KeyCode.F) && !isRotating && (enter || area2DEnter))
        {
            isRotating = true;
            rotationAxis = Vector3.forward;
            rotationAngle = 180.0f;
            rotationAngle1 = 90f;
        }

        // 正在旋转时绕着B的z轴旋转
        if (isRotating)
        {
            // 计算旋转角度
            float angle = rotationSpeed * Time.deltaTime;
            float angle1 = rotationSpeed1 * Time.deltaTime;

            // 判断是否旋转到目标角度
            if (rotationAngle - angle <= 0.0f && rotationAngle1 - angle1 <= 0.0f)
            {
                angle = rotationAngle;
                angle1 = rotationAngle1;
                isRotating = false;
            }

            // 绕着B的z轴旋转
            objectA.transform.RotateAround(objectB.transform.position, -rotationAxis, angle);
            //绕着D的z轴旋转
            objectC.transform.RotateAround(objectD.transform.position, rotationAxis, angle1);

            // 更新旋转角度
            rotationAngle -= angle;
            rotationAngle1 -= angle1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            enter = true; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            enter = false;
        }
    }
}
