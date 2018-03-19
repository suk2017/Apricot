using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    public class VerticalRotate : MonoBehaviour
    {
        [Tooltip("平滑度")] public float smoothTime = 0.1f;
        [Tooltip("灵敏度")] public float speed = 2;
        [Tooltip("是否限制在min和max之间")] public bool clamp = false;
        [Tooltip("下限[-90,90)")] public float max = 60;
        [Tooltip("上限(-90,90]")] public float min = -85;

        private Quaternion target;
        private Transform tr;
        private float value;
        private bool running;

        private void Start()
        {
            tr = transform;
            target = tr.localRotation;
            max = -max;
            min = -min;

        }


        private void Update()
        {
            //float r = -RoamManager.verticalRotateValue() * speed;
            float r = value * speed;
            if (max > min)
            {
                float temp = min;
                min = max;
                max = temp;
            }
            if (clamp)
            {
                /* r < (90-max) 多0.1也可以
                 * 这是为了防止冲破阻碍（冲过去就回不来了）
                 * 在任何角度都不能达到这个速率临界值 
                 * 否则请使用 当前x轴转换后的角度α
                 * 即 r < (90-α)
                 */
                r = ((min + r) > 90) ? (90 - min) : r;//速率不能超过速率上限
                r = ((max + r) < -90) ? (-90 - max) : r;//速率不能超过速率下限

                target *= Quaternion.Euler(r, 0, 0);//执行旋转
                Vector3 v = target.eulerAngles;
                //v.x = v.x > 180 ? 0 : v.x;
                if (v.x < 180)
                {
                    v.x = v.x > min ? min : v.x;//
                }
                else
                {
                    v.x = (v.x - 360) < max ? (max + 360) : v.x;
                }
                target = Quaternion.Euler(v);
            }
            else
            {
                target *= Quaternion.Euler(r, 0, 0);
            }
            tr.localRotation = Quaternion.Slerp(tr.localRotation, target, smoothTime);
        }

        public void _Update(float v)
        {
            value = running ? v : 0;
        }

        public void _SetRunning(bool r)
        {
            running = r;
        }

        public void _SetTarget(Quaternion qtn)
        {
            target = qtn;
        }
    }
}
