using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshMobileFPMove : MonoBehaviourPun
{
    private Vector3 m_velocity;
    private float m_angle = 0.0f;
    private bool m_isGrounded = true;
    private bool m_jumpOn = false;

    //for Animation
    public Animator Animator;
    CharacterController character;

    public cshJoystick sJoystick;
    public float m_moveSpeed = 2.0f; //보통 걷는 속도
    private float m_runSpeed = 4.0f; //걷다가 뛰는 속도
    private float mySpeed = 0.0f; //내 default속도
    public float m_jumpForce = 5.0f;

    //float ry;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponentInParent<PhotonView>().IsMine)
            return;

        character = GetComponent<CharacterController>();

        mySpeed = m_moveSpeed; //내 기본 속도 = 걷는 속도
        m_runSpeed = mySpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponentInParent<PhotonView>().IsMine)
            return;

        PlayerMove();
    }
    
    public void OnVirtualPadJump()
    {
        if (this == null) { return; }
        const float rayDistance = 0.5f;
        var ray = new Ray(transform.localPosition + new Vector3(0.0f, 0.1f, 0.0f), Vector3.down);
       // if (Physics.Raycast(ray, rayDistance))
            //{
            //    Debug.Log("Jump true");
            //    m_jumpOn = true;
            //}
            m_jumpOn = true;
        Debug.Log("Jump");
    }

    /*
    public void OnVirtualPadShoot()
    {
        GetComponentInChildren<cshMobileRaySpawn>().Shoot();
    }

    //Reload버튼을 눌렀을 시 아래 함수 호출
    public void OnVirtualPadReload()
    {
        //자식 객체 중 cshMobileRaySpawn컴포넌트를 가지고 있는 객체를 가져와서 Reload()함수 호출 
        GetComponentInChildren<cshMobileRaySpawn>().Reload();
    }
    */

    private void PlayerMove()
    {
        //Walk -> Run 변환
        if (sJoystick.time > 2.0f) //2초가 흘렀다면
            mySpeed = m_runSpeed; //4
        else
            mySpeed = m_moveSpeed; //2

        CharacterController controller = GetComponent<CharacterController>();
        float gravity = 20.0f;

        if (controller.isGrounded)
        {
            float h = sJoystick.GetHorizontalValue();
            float v = sJoystick.GetVerticalValue();

            //3인칭 애니메이션
            Animator.SetFloat("directionX", h);
            Animator.SetFloat("directionY", v);

            /*
            m_velocity = new Vector3(h, 0, v);
            */
            Camera[] cams = GetComponentsInChildren<Camera>();
            Transform camDir = transform;
            for (int i = 0; i < cams.Length; i++)
            {
                if (cams[i].gameObject.activeSelf == true)
                {
                    camDir = cams[i].gameObject.transform;
                }
            }

            m_velocity = camDir.forward * v + camDir.right * h;
            m_velocity = m_velocity.normalized;



            /*
            // h: Rotation, v: Move
            //m_angle = h * 1.0f;
            ry += 200.0f * h * Time.deltaTime;
            

            m_velocity = transform.forward * v;
            m_velocity = m_velocity.normalized;
            */

            if (m_jumpOn)
            {
                m_velocity.y = m_jumpForce;
                m_jumpOn = false;
            }
            /*
            else if (m_velocity.magnitude > 0.5)
            {
                transform.LookAt(transform.position + m_velocity);
            }*/
        }

        m_velocity.y -= gravity * Time.deltaTime;
        //transform.Rotate(new Vector3(0, 1, 0) * m_angle, Space.Self);
        //transform.eulerAngles = new Vector3(0, ry, 0);
        controller.Move(m_velocity * mySpeed * Time.deltaTime);

        //캐릭터 모델을 이동방향으로 바라보도록 설정
        // Vector3 look = new Vector3(m_velocity.x, 0.0f, m_velocity.z).normalized;
        //Player.LookAt(Player.position + look);


        m_isGrounded = controller.isGrounded;

        //For walk animation
        Animator.SetFloat("isMoving", character.velocity.magnitude * mySpeed); //Move의 길이
    }
}
