using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool lockPosY = true;


    private Vector3 offset;


    void Start()
    {
        //设置相对偏移
        offset = target.position - this.transform.position;
    }

    void Update()
    {
        //更新位置
        if (lockPosY)
        {
            var tmpPos = target.position - offset;
            this.transform.position = new Vector3(tmpPos.x, this.transform.position.y, tmpPos.z);
        }
        else
        {
            this.transform.position = target.position - offset;
        }
    }
}
