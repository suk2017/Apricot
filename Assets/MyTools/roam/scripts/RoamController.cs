/**************************
 * 这部分代码可以复制一份再更改 尤其是操作部分
 * 本类向外提供API 
 **************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    public class RoamController : MonoBehaviour
    {

        /************** 组件 **************/
        [Tooltip("目标旋转+目标移动")] public CameraMove cameraMove;
        [Tooltip("主机旋转H")] public HorizontalRotate main_horizontalRotate;
        [Tooltip("主机旋转V")] public VerticalRotate main_verticalRotate;
        [Tooltip("相机拖拉")] public CameraDrag cameraDrag;
        [Tooltip("副机旋转H")] public HorizontalRotate vice_horizontalRotate;
        [Tooltip("副机旋转vVB")] public Transform vice_verticalRotate_Base;
        [Tooltip("副机旋转V")] public VerticalRotate vice_verticalRotate;

        /************** 目标 **************/
        public Transform target;//目标

        /************** 加速 **************/
        private Transform m_cameraMove_Tr;
        private Transform m_main_horizontalRotate_Tr;
        private Transform m_main_verticalRotate_Tr;
        private Transform m_cameraDrag_Tr;
        private Transform m_vice_horizontalRotate_Tr;
        private Transform m_vice_verticalRotate_Base_Tr;
        private Transform m_vice_verticalRotate_Tr;


        /************** 状态 **************/
        //private bool m_free_view = false;//自由视角：水平，垂直，拖拉
        //private bool m_vice_view = false;//副机 自由视角：水平，垂直
        //private bool m_move_free = false;//自由移动：水平面
        //private bool m_follow = false;//跟随目标（一次定位 每帧跟随）



        /************** 私有方法 **********/



        private void Awake()
        {
            /*******子类代码的初始化******/
            cameraMove._SetTarget(target);
            //cameraMove._SetManager(this);

            //cameraMoveFree._SetManager(this);



            /*******自己代码的初始化*******/
            m_cameraMove_Tr = cameraMove.transform;
            m_main_horizontalRotate_Tr = main_horizontalRotate.transform;
            m_main_verticalRotate_Tr = main_verticalRotate.transform;
            m_cameraDrag_Tr = cameraDrag.transform;
            m_vice_horizontalRotate_Tr = vice_horizontalRotate.transform;
            m_vice_verticalRotate_Base_Tr = vice_verticalRotate_Base.transform;
            m_vice_verticalRotate_Tr = vice_verticalRotate.transform;
        }

        public IEnumerator Mode_Fixed()
        {
            yield return null;
            StartCoroutine(Mode_Free());
            StopCoroutine(Mode_Fixed());
        }
        public IEnumerator Mode_Free()
        {
            yield return null;
        }


        void Update()
        {
            /**************** 主机自由视角：移动 旋转 拖拉 ****************/
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0))
            {
                main_horizontalRotate._SetRunning(true);
                main_verticalRotate._SetRunning(true);
            }
            else if ((Input.GetKeyUp(KeyCode.Z) || Input.GetMouseButtonUp(0)))
            {
                main_horizontalRotate._SetRunning(false);
                main_verticalRotate._SetRunning(false);
            }
            main_horizontalRotate._Update(Input.GetAxis("Mouse X"));
            main_verticalRotate._Update(-Input.GetAxis("Mouse Y"));
            cameraDrag._Target(Input.GetAxis("Mouse ScrollWheel"));

            /**************** 副机自由视角：移动 旋转 ****************/
            if (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(1))
            {
                vice_horizontalRotate._SetRunning(true);
                vice_verticalRotate._SetRunning(true);
            }
            else if (Input.GetKeyUp(KeyCode.X) || Input.GetMouseButtonUp(1))
            {
                vice_horizontalRotate._SetRunning(false);
                vice_verticalRotate._SetRunning(false);
            }
            vice_horizontalRotate._Update(Input.GetAxis("Mouse X"));
            vice_verticalRotate._Update(-Input.GetAxis("Mouse Y"));

            /**************** 副机旋转调整 归正 ****************/
            if (Input.GetKeyDown(KeyCode.X))//若要归正
            {
                vice_horizontalRotate._SetTarget(Quaternion.identity);
                vice_verticalRotate._SetTarget(Quaternion.identity);
            }
            else
            {
                float rot = m_main_verticalRotate_Tr.localRotation.eulerAngles.x;
                m_cameraDrag_Tr.localRotation = Quaternion.Euler(-rot, 0, 0);
                m_vice_verticalRotate_Base_Tr.localRotation = Quaternion.Euler(rot, 0, 0);
            }

        }

        /**********************************/

        /// <summary>
        /// 获取当前朝向
        /// </summary>
        public Quaternion _GetToward()
        {
            return m_main_horizontalRotate_Tr.localRotation;
        }

    }
}