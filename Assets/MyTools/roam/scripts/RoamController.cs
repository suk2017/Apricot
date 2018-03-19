/**************************
 * �ⲿ�ִ�����Ը���һ���ٸ��� �����ǲ�������
 * ���������ṩAPI 
 **************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTools.roam
{
    public class RoamController : MonoBehaviour
    {

        /************** ��� **************/
        [Tooltip("Ŀ����ת+Ŀ���ƶ�")] public CameraMove cameraMove;
        [Tooltip("������תH")] public HorizontalRotate main_horizontalRotate;
        [Tooltip("������תV")] public VerticalRotate main_verticalRotate;
        [Tooltip("�������")] public CameraDrag cameraDrag;
        [Tooltip("������תH")] public HorizontalRotate vice_horizontalRotate;
        [Tooltip("������תvVB")] public Transform vice_verticalRotate_Base;
        [Tooltip("������תV")] public VerticalRotate vice_verticalRotate;

        /************** Ŀ�� **************/
        public Transform target;//Ŀ��

        /************** ���� **************/
        private Transform m_cameraMove_Tr;
        private Transform m_main_horizontalRotate_Tr;
        private Transform m_main_verticalRotate_Tr;
        private Transform m_cameraDrag_Tr;
        private Transform m_vice_horizontalRotate_Tr;
        private Transform m_vice_verticalRotate_Base_Tr;
        private Transform m_vice_verticalRotate_Tr;


        /************** ״̬ **************/
        //private bool m_free_view = false;//�����ӽǣ�ˮƽ����ֱ������
        //private bool m_vice_view = false;//���� �����ӽǣ�ˮƽ����ֱ
        //private bool m_move_free = false;//�����ƶ���ˮƽ��
        //private bool m_follow = false;//����Ŀ�꣨һ�ζ�λ ÿ֡���棩



        /************** ˽�з��� **********/



        private void Awake()
        {
            /*******�������ĳ�ʼ��******/
            cameraMove._SetTarget(target);
            //cameraMove._SetManager(this);

            //cameraMoveFree._SetManager(this);



            /*******�Լ�����ĳ�ʼ��*******/
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
            /**************** ���������ӽǣ��ƶ� ��ת ���� ****************/
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

            /**************** ���������ӽǣ��ƶ� ��ת ****************/
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

            /**************** ������ת���� ���� ****************/
            if (Input.GetKeyDown(KeyCode.X))//��Ҫ����
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
        /// ��ȡ��ǰ����
        /// </summary>
        public Quaternion _GetToward()
        {
            return m_main_horizontalRotate_Tr.localRotation;
        }

    }
}