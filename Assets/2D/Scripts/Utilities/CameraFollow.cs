using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnightAdventure
{

    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public Vector3 offset;
        public float distance = 10;
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

        void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            ////更新位置
            //if (lockPosY)
            //{
            //    var tmpPos = target.position - offset;
            //    this.transform.position = new Vector3(tmpPos.x, this.transform.position.y, tmpPos.z);
            //}
            //else
            //{
            //    this.transform.position = target.position - offset;
            //}

            Vector3 viewPoint = Camera.main.WorldToViewportPoint(target.position, Camera.MonoOrStereoscopicEye.Mono);
            //Debug.Log(viewPoint);
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }

}