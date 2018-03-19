using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScreen : MonoBehaviour {

    public RectTransform canvasRectTransform;
    private void Start()
    {
        float sw = Screen.width;
        float sh = Screen.height;
        if (Mathf.Abs(sw / sh - 1920f / 1080) > 0.01f)
        {
            //canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
            Vector3 r = canvasRectTransform.localScale;
            canvasRectTransform.localScale = new Vector3(r.x * (sw * 0.5625f / sh), r.y, r.z);
        }
    }
}
