/**************************
 * 包括主机的移动和旋转
 **************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    public class CameraMove : MonoBehaviour
    {
        public float smoothTime_move = 0.1f;
        public float smoothTime_rotate = 0.1f;
        public bool lockMove = false;//锁定移动
        public bool lockView = false;//锁定旋转

        private Transform target;//追踪目标
        private Transform tr;



        /**************** 私有方法 ********************/
        private void Awake()
        {
            tr = this.transform;
        }




        private void FixedUpdate()
        {
            if (target)
            {
                if (lockMove) { tr.position = Vector3.Lerp(tr.position, target.position, smoothTime_move); }//位置
                if (lockView) { tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, smoothTime_rotate); }//旋转
            }
            else
            {
                print("未加载target");
            }
        }

        /************* 公有方法 *************/


        /// <summary>
        /// 只改变自己 且假定只有一个子物体
        /// </summary>
        public void RotateSelf()
        {
            if (!target) { return; }
            Transform t = transform.GetChild(0);
            transform.DetachChildren();
            transform.rotation = target.rotation;
            t.parent = transform;
        }






        /// <summary>
        /// 设置目标
        /// </summary>
        public void _SetTarget(Transform tr)
        {
            target = tr;
        }



    }
}
