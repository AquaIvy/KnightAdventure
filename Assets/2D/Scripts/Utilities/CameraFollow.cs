using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool lockPosY = true;




    void Start()
    {
        if (target == null)
        {
            return;
        }

        //设置相对偏移
        offset = target.position - this.transform.position;
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

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
