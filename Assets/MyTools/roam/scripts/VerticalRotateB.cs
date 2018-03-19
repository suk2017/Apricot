using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalRotateB : MonoBehaviour {

    public float speed = 1;
    public float smoothTime = 1;

    private Quaternion target;
    private Transform tr;

    private void Start()
    {
        tr = transform;
    }
    //TODO 现在暂时使用临时方法进行控制 之后进行优化
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("right button down");
            target *= Quaternion.Euler(Input.GetAxis("Vertical") * speed, 0, 0);
        }
        tr.localRotation = Quaternion.Slerp(tr.localRotation, target, smoothTime);
        
    }
}
