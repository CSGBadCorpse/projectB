using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerMovemenSO : ScriptableObject
{
    [Header("跳跃参数")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    [Header("地面检测")]
    public LayerMask groundLayer;
    [Header("跳跃距离检测")]
    public float jumpDistance;
    [Header("移动速度")]
    public float moveSpeed;
    [Header("移动操作微调参数")]
    public float offset;
    /*[Header("角色本地坐标")]
    public Transform localTransform;*/
    [Header("旋转速度")]
    public float rotationSpeed;
    [Header("地面摩擦力")]
    public float groundDrag;
    [Header("道具获取")]
    public string triggerTagName;
}
