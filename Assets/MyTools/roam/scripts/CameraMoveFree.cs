using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    /// <summary>
    /// 此类既实现了 目标追踪又可以自由移动
    /// </summary>
    public class CameraMoveFree : MonoBehaviour
    {
        public float smoothTime = 0.05f;
        public float speed = 0.2f;

        private RoamController m_roamController;
        private Vector3 target = Vector3.zero;//虚拟目标 不显示在屏幕上



        /***************************************************/



        private void OnEnable()
        {
            target = transform.position;//每次进入Free Move就要重新设定初始值
        }




        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target, smoothTime);
        }



        /***************************************/
        


        /// <summary>
        /// 设置管理器
        /// </summary>
        public void _SetManager(RoamController rc)
        {
            m_roamController = rc;
        }



        /// <summary>
        /// 改变目标值
        /// </summary>
        /// <param name="value"></param>
        public void _Target(Vector3 value)
        {
            target += m_roamController._GetToward() * value * speed;//四元数是标正方向 而且必须左乘
        }
    }
}