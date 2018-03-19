using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    public class HorizontalRotate : MonoBehaviour
    {
        [Tooltip("平滑度")] public float smoothTime = 0.1f;
        [Tooltip("灵敏度")] public float speed = 2;

        private Transform tr;
        private Quaternion target;
        private float value;
        private bool running = false;

        private void Start()
        {
            tr = this.transform;
            target = transform.localRotation;
        }
        //private void Update()
        //{
        //   target *= Quaternion.Euler(0, RoamManager.horizontalRotateValue() * speed, 0);
        //    transform.localRotation = Quaternion.Slerp(transform.localRotation, target, smoothTime);
        //}

        public void Update()
        {
            target *= Quaternion.Euler(0, value * speed, 0);
            tr.localRotation = Quaternion.Slerp(tr.localRotation, target, smoothTime);
        }

        public void _Update(float v)
        {
            value = running ? v : 0f;
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
