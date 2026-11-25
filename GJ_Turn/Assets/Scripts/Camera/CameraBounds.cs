using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraBounds : MonoBehaviour
{
    [Header("边界")]
    public BoxCollider2D _Coll ;
    private Transform PlayerTransform;
    Transform playerTransform;

    Camera mainCamera ;

    float left ;
    float right ;
    float up ;
    float down ;
    float Height;
    float Width;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Height = mainCamera.orthographicSize;
        Width = Height * mainCamera.aspect;
        //获取相关边界
        left = _Coll.bounds.min.x + Width;
        right = _Coll.bounds.max.x - Width;
        down = _Coll.bounds.min.y + Height;
        up = _Coll.bounds.max.y - Height;
        //获取玩家
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float CameraX;
        float CameraY;
        if (left > right) //防止边界太小导致bug
        {
            CameraX = _Coll.bounds.center.x;
        }
        else
        {
            CameraX = Mathf.Clamp(playerTransform.position.x, left, right); //判断摄像机要到的位置
        }
        if (down > up)
        {
            CameraY = _Coll.bounds.center.y;
        }
        else
        {
            CameraY = Mathf.Clamp(playerTransform.position.y, down, up);
        }
        
        transform.position = new Vector3 (CameraX, CameraY, transform.position.z);
        
    }
}
