using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshPCFPMove : MonoBehaviourPun
{
    //public float speed = 5;
    public float gravity = -9.81f;
    public float jumpPower = 2;
    public float m_moveSpeed = 2.0f; //보통 걷는 속도
    private float m_runSpeed = 4.0f; //걷다가 뛰는 속도
    private float mySpeed = 0.0f; //내 default속도
    private float time = 0.0f;
    private float timeRun = 2.0f;

    private Animator FPAnimator;
    private Animator TPAnimator;
    CharacterController character;
    float yVelocity;

    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponentInParent<PhotonView>().IsMine)
            return;

        Animator[] anims = GetComponentsInChildren<Animator>();

        character = GetComponent<CharacterController>();
        FPAnimator = anims[0];
        TPAnimator = anims[1];

        mySpeed = m_moveSpeed; //내 기본 속도 = 걷는 속도
        m_runSpeed = mySpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponentInParent<PhotonView>().IsMine)
            return;

        //--- 점프 ----
        //[점프]1. y속도에 중력을 계속 더한다.
        yVelocity += gravity * Time.deltaTime;
        //[점프]2. 만약 사용자가 점프버튼을 누르면 y속도에 뛰는 힘을 대입한다.
        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
        }

        if (!Input.GetKey("up") && !Input.GetKey("down") &&
            !Input.GetKey("right") && !Input.GetKey("left"))
        {
            mySpeed = m_moveSpeed;
            time = 0.0f;
        }
        else
        {
            //---속도 변환---
            time += Time.deltaTime;
            if (time >= timeRun) mySpeed = m_runSpeed;
        }

        //--- 이동 ----
        // 1. 사용자의 입력에 따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 앞뒤 좌우로 방향을 만든다.
        Vector3 dir = Vector3.right * h + Vector3.forward * v;

        //카메라가 보고있는 방향을 앞 방향으로 변경한다.
        dir = GetComponentInChildren<Camera>().gameObject.transform.TransformDirection(dir);

        //로컬스페이스에서 월드스페이스로 변환 해준다. (트렌스폼 기준으로 결과를 바꾼다.)

        //대각선 이동으로 하면서 루트2로 길이가 늘어나기에 1로 만들어준다. (정규화:Normalize)
        dir.Normalize();

        //[점프]3. y속도를 최종 dir의 y에 대입한다.
        dir.y = yVelocity;

        // 3. 그 방향으로 이동한다.
        // P = P0 + vt
        //transform.position += dir * speed * Time.deltaTime;

        character.Move(dir * mySpeed * Time.deltaTime);
        //Move 움직이전에 충돌 체크를 해준다. 만약 충돌한다면 멈춘다.


        //캐릭터 모델을 이동방향으로 바라보도록 설정
        //Vector3 look = new Vector3(dir.x, 0.0f, dir.z).normalized;
        //Player.LookAt(Player.position + look);


        //For walk animation
        //Animator.SetFloat("Move", character.velocity.magnitude * mySpeed);
        TPAnimator.SetFloat("directionX", h);
        TPAnimator.SetFloat("directionY", v);
        Debug.Log(character.velocity.magnitude * mySpeed);
    }
}
