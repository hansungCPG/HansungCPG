using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshPCRotate : MonoBehaviour
{
    float rx;
    float ry;
    [SerializeField] float m_Speed;
    public float rotSpeed = 200;
    public Transform follow;

    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //1. 마우스 입력 값을 이용한다.
        float mx = Input.GetAxis("Mouse X"); //게임창에서 마우스를 왼쪽 오른쪽으로 이동할때 마다 (왼 -음수 : 오른 +양수)
        float my = Input.GetAxis("Mouse Y"); //게임창에서 마우스를 왼쪽 오른쪽으로 이동할때 마다 (아래 -음수 : 위 +양수)

        rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        Quaternion q = follow.rotation; //rotation변수 = Quaternion
        Quaternion q2 = q; //q2 = temp 역할(치환용)
        q.eulerAngles = new Vector3(q.eulerAngles.x + -my * m_Speed, q.eulerAngles.y + mx * m_Speed, q.eulerAngles.z);
        Vector3 look = new Vector3(q.x, 0.0f, q.z).normalized;
        //Player.LookAt(Player.position + look);

        q2.x = transform.parent.parent.transform.rotation.x; //FPCamera.x = body.x
        q2.z = transform.parent.parent.transform.rotation.z; //FPCamera.x = body.x
        transform.parent.parent.transform.rotation = q2; //body의 rotation = FPCamera의 rotation (y축을 중심으로 움직인다)

        //rx 회전 각을 제한 (화면 밖으로 마우스가 나갔을때 x축 회전 덤블링 하듯 계속 도는 것을 방지)
        //rx = Mathf.Clamp(rx, -80, 80);
        //x을 돌리는 이유 x축이 이동이 아니라 x축을 회전 해서 위아래 보는 방향은 x축이여야 한다.

        //2. 회전을 한다.
        //transform.eulerAngles = new Vector3(-rx, ry, 0);
        //X축의 회전은 양수가 증가되면 아래, 음수가 증가되면 위로 돌아간다. (그래서 x축을 -를 넣었다)

        // 1인칭 카메라일때, x축으로의 회전 범위 제한설정
        float clampx = q.eulerAngles.x;
        if (q.eulerAngles.x >= 180.0f) clampx = q.eulerAngles.x - 360.0f;
        clampx = Mathf.Clamp(clampx, -40.0f, 60.0f);

        q.eulerAngles = new Vector3(clampx, q.eulerAngles.y, q.eulerAngles.z);
        follow.rotation = q;

    }
}
