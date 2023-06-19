using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    public GameObject objectA; // 游戏物体A
    public GameObject objectB; // 游戏物体B

    public float rotationSpeed; // 旋转速度

    private bool isRotating; // 是否正在旋转
    private Vector3 rotationAxis = Vector3.forward; // 旋转轴向
    private float rotationAngle; // 旋转角度

    private bool enter;

    private void Start()
    {
        enter = false;
        objectA.transform.RotateAround(objectB.transform.position, rotationAxis, 180f);
    }

    private void Update()
    {
        // 按下f键时开始旋转
        if (Input.GetKeyDown(KeyCode.F) && !isRotating)
        {
            isRotating = true;
            rotationAxis = Vector3.forward;
            rotationAngle = 90.0f;
        }

        // 正在旋转时绕着B的z轴旋转
        if (isRotating)
        {
            // 计算旋转角度
            float angle = rotationSpeed * Time.deltaTime;

            // 判断是否旋转到目标角度
            if (rotationAngle - angle <= 0.0f)
            {
                angle = rotationAngle;
                isRotating = false;
            }

            // 绕着B的z轴旋转
            objectA.transform.RotateAround(objectB.transform.position, rotationAxis, angle);

            // 更新旋转角度
            rotationAngle -= angle;
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
