using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerControl : MonoBehaviour
{
    private static PlayerControl instance;
    public static PlayerControl Instance => instance;

    public float moveSpeed = 5f; // 玩家移动速度
    public float raycastDistance = 0.5f;
    private bool canMove = true;
    private bool isCollison = false;
    private GameObject collisionObj;
    private GameObject hitObj;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // 获取输入
        float moveHorizontal = Input.GetAxis("Horizontal"); // A和D键
        float moveVertical = Input.GetAxis("Vertical"); // W和S键

        // 创建一个移动向量
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);
        CanMove(movement);

        if (canMove)
        {
            transform.position += movement * moveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        isCollison = true;
        //canMove = false;
        collisionObj = other.gameObject;
    }

    private void OnCollisionExit(Collision other)
    {
        isCollison = false;
        collisionObj = null;
    }


    private void OnDrawGizmos()
    {
        // 获取输入
        float moveHorizontal = Input.GetAxis("Horizontal"); // A和D键
        float moveVertical = Input.GetAxis("Vertical"); // W和S键

        // 创建一个移动向量
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0);

        // 如果移动向量不为零，则绘制射线
        if (movement != Vector3.zero)
        {
            // 获取移动方向
            Vector3 direction = movement.normalized;
            // 计算射线的起点
            Vector3 rayOrigin = transform.position; // 稍微偏移一点，避免射线从物体内部穿过

            // 绘制射线
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayOrigin, direction);

      
        }
    }

    private void CanMove(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            // 获取移动方向
            Vector3 direction = movement.normalized;
            // 计算射线的起点
            RaycastHit hit;

            // 如果射线检测到物体，则不移动
            if (Physics.CapsuleCast(transform.position, Vector3.one, 0.5f, direction, out hit, raycastDistance))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
                hitObj = hit.collider.gameObject;
                if (hit.collider.CompareTag("Obstacle"))
                {
                    canMove = false;
                }
            }
            else
            {
                canMove = true;
            }
        }
        else
        {
            hitObj = null;
            canMove = true;
        }
    }
}