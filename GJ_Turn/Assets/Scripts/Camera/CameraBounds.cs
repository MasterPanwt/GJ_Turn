using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraBounds : MonoBehaviour
{
    
    public BoxCollider2D _Coll ;
    [Header("玩家")]
    public Transform PlayerTransform;

    Camera mainCamera ;

    float left ;
    float right ;
    float up ;
    float down ;

    const int Height = 5;
    const int Width = 9;
    // Start is called before the first frame update
    void Start()
    {
        
        //获取相关边界
        left = _Coll.bounds.min.x;
        right = _Coll.bounds.max.x;
        down = _Coll.bounds.min.y;
        up = _Coll.bounds.max.y;
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //如果玩家的左右边某位置在范围内
        if (PlayerTransform.position.x - Width >= left &&
            PlayerTransform.position.x + Width <= right )
        {
            transform.position = new Vector3 (PlayerTransform.position.x , transform.position.y ,-10);

        }
        if (PlayerTransform.position.y - Height >= down &&
            PlayerTransform.position.y + Height <= up )
        {
            Debug.Log("y方向的判断");
            transform.position = new Vector3 (transform.position.x , PlayerTransform.position.y,-10);
        }
        
    }
}
