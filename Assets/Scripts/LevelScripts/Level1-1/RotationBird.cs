using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBird : MonoBehaviour
{
    [SerializeField]
    [Header("围绕点")]
    private Transform target;
    [SerializeField]
    [Header("原位")]
    private Transform originPos;
    [SerializeField]
    private PlayerController controller;
    private bool returnPos = false;

    public float speed = 5f;
    public float targetRotationY = 90f;

    private Vector3 targetDirection;

    // 旋转动画协程
    IEnumerator RotateToOriginPosition()
    {
        // 获取物体的当前旋转角度
        float initialRotationY = transform.rotation.eulerAngles.y;

        // 将物体local forward指向目标方向
        Quaternion targetRotation = Quaternion.Euler(0f, targetRotationY, 0f);
        transform.rotation = targetRotation;

        // 等待旋转动画完成
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            float rotationY = Mathf.Lerp(initialRotationY, targetRotationY, t);
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
            yield return null;
        }

        // 取消当前脚本的激活状态
        this.enabled = false;
        controller.enabled = true;
    }

    void Update()
    {
        if (!returnPos)
        {
            Vector3 relativePos = (target.position + new Vector3(0, -.4f, 0)) - transform.position;

            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Quaternion current = transform.localRotation;

            transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
            transform.Translate(0, 0, 3 * Time.deltaTime);
        }
        else if (returnPos)
        {
            // 计算物体与目标点之间的向量
            Vector3 direction = originPos.position - transform.position;

            // 如果距离小于一个阈值，则认为已到达目标点
            if (direction.magnitude < 0.01f)
            {
                // 开始旋转动画协程
                StartCoroutine(RotateToOriginPosition());
                return;
            }

            // 计算物体需要旋转的角度，使用Quaternion.LookRotation计算目标旋转
            targetDirection = direction.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // 根据物体朝向目标点的方向和移动速度计算移动的距离
            float remainingDistance = direction.magnitude;
            Vector3 moveDistance = targetDirection * speed * Time.deltaTime;

            // 使用RotateTowards方法来平滑地旋转物体。
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
            transform.rotation = newRotation;

            // 移动物体
            transform.Translate(moveDistance, Space.World);
        }
    }

    public void ReturnPosition()
    {
        returnPos = true;
    }
}