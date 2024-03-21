using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshARFPMove : MonoBehaviourPun
{
    private Vector3 m_velocity;

    public cshJoystick sJoystick;
    public float mySpeed = 4.0f; //내 default속도

    bool dragState; // Joystick을 터치하고 있는지 여부를 확인
    float rx;
    public float rotSpeed = 200;

    public Transform Player;
    private Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponentInParent<PhotonView>().IsMine)
            return;
        thisTransform = GetComponent<Transform>();
        //Animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponentInParent<PhotonView>().IsMine)
            return;

        CharacterController controller = GetComponent<CharacterController>();
        //float gravity = 20.0f;

        float h = sJoystick.GetHorizontalValue();
        float v = sJoystick.GetVerticalValue();
        m_velocity = new Vector3(h, 0, v);
        m_velocity = m_velocity.normalized;

        if (m_velocity.magnitude > 0.5) transform.LookAt(transform.position + m_velocity);

        //m_velocity.y -= gravity * Time.deltaTime;
        controller.Move(m_velocity * mySpeed * Time.deltaTime);

        Debug.Log("Move");

        dragState = sJoystick.GetComponent<cshJoystick>().onDrag;
        Quaternion q = thisTransform.rotation;
        if (Input.GetMouseButton(0) && !dragState)
        {
            //마우스 이동에 따라 AR케릭터 위,아래로 이동
            float my = Input.GetAxis("Mouse Y"); //게임창에서 마우스를 왼쪽 오른쪽으로 이동할때 마다 (아래 -음수 : 위 +양수)

            rx += rotSpeed * my * Time.deltaTime;

            rx = Mathf.Clamp(rx, -45, 45);

            //transform.eulerAngles = new Vector3(-rx, q.y, 0);
            //X축만 회전하고 Y축은 원래 회전값을 유지하는 형태로 위, 아래로 이동하게 하는 코드
            float tr;
            tr = transform.rotation.eulerAngles.y;
            transform.eulerAngles = new Vector3(-rx, tr, 0); //같은 오일러 값으로 넣어주기 


            Debug.Log("rotation");
        }
    }
}
