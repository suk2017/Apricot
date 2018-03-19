/*
 * 通过 API 调用来调整
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    public class CameraDrag : MonoBehaviour
    {
        [Tooltip("平滑度")] public float smoothTime = 0.1f;
        [Tooltip("灵敏度")]public float speed = 2;
        [Tooltip("是否限制在min和max之间")] public bool clamp = true;
        [Tooltip("下限[-90,90)")] public float min = -10;
        [Tooltip("上限(-90,90]")]public float max = 0;
        [Tooltip("与Position的z值挂钩")]public float target;

        private Transform tr;
        public Camera m_camera;
        



        /*****************************************/



        private void Awake()
        {
            tr = transform;
        }



        //在awake后 start前执行
        private void OnEnable()
        {
            //target = m_tr.localPosition.z;
        }



        private void OnDisable()
        {
            if (m_camera.orthographic)
            {
                target = -m_camera.orthographicSize;
            }
            else
            {
                target = tr.localPosition.z;
            }
        }



        private void Update()
        {
            if (clamp)
            {
                target = target > max ? max : target;
                target = target < min ? min : target;
            }
            if (m_camera.orthographic)
            {
                m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, -target, smoothTime);
            }
            else
            {
                tr.localPosition = new Vector3(0, 0, Mathf.Lerp(tr.localPosition.z, target, smoothTime));
            }
        }



        /***********************************************/



        /// <summary>
        /// target += value ...
        /// </summary>
        public void _Target(float value)
        {
            if (enabled)//如果脚本未被暂停
            {
                //变量 * 权值 * 常量 中间是为了在慢的时候减慢 快的时候加快
                target += value * (Mathf.Abs(target) + 0.1f) * speed;
            }
        }



        /// <summary>
        /// 改变 camera.orthographic
        /// </summary>
        public void _ChangeMode()
        {
            //TODO 通过创建协程 来使画面变化更流畅
            if (m_camera.orthographic)
            {
                m_camera.orthographic = false;
                tr.localPosition = new Vector3(0, 0, target);//target直接赋值 截断防止z位置继续变化
                print("改变相机模式：透视");
            }
            else
            {
                m_camera.orthographic = true;
                m_camera.orthographicSize = -target;//target直接复制 截断防止size继续变化
                tr.localPosition = new Vector3(0, 0, -1000);
                print("改变相机模式：正交");
            }
        }
    }
}