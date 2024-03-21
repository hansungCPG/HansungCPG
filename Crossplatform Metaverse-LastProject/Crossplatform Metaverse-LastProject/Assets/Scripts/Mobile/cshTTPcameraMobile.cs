using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshTTPcameraMobile : MonoBehaviour
{
    public Transform follow;
    [SerializeField] float m_Speed;
    public float rotSpeed = 0.3f;

    bool mouseState = false;

    bool dragState; // Joystick을 터치하고 있는지 여부를 확인

    public GameObject joystickArea;

    public Transform Player;
    void Start()
    {

    }

    public void LateUpdate()
    {
        dragState = joystickArea.GetComponent<cshJoystick>().onDrag;
        if (Input.GetMouseButton(0) && !dragState)
            Rotate();
    }
    public void Rotate()
    {
        float mx = Input.GetAxis("Mouse X"); //게임창에서 마우스를 왼쪽 오른쪽으로 이동할때 마다 (왼 -음수 : 오른 +양수)
        float my = Input.GetAxis("Mouse Y"); //게임창에서 마우스를 왼쪽 오른쪽으로 이동할때 마다 (아래 -음수 : 위 +양수)
        if (Input.touchCount > 0)
        {
            mx = Input.touches[0].deltaPosition.x;
            my = Input.touches[0].deltaPosition.y;
        }

        Quaternion q = follow.rotation; //rotation변수선언 용 = Quaternion
        Quaternion q2 = q; //q2 = temp 역할(치환용)
        //eulerAngles = 오일러 각: 180또는 -180을 초과하는 값에 대해서 그 값에 대한 네거티브값으로 취급한다.
        q.eulerAngles = new Vector3(q.eulerAngles.x + my * m_Speed, q.eulerAngles.y + -mx * m_Speed, q.eulerAngles.z);
        Vector3 look = new Vector3(q.x, 0.0f, q.z).normalized;
        //Player.LookAt(Player.position + look);

        q2.x = transform.parent.parent.transform.rotation.x; //FPCamera.x = body.x
        q2.z = transform.parent.parent.transform.rotation.z; //FPCamera.z = body.z
        transform.parent.parent.transform.rotation = q2; //body의 rotation = FPCamera의 rotation (y축을 중심으로 움직인다)
    

        // 1인칭 카메라일때, x축으로의 회전 범위 제한설정
        float clampx = q.eulerAngles.x;
        if (q.eulerAngles.x >= 180.0f) clampx = q.eulerAngles.x - 360.0f;
        clampx = Mathf.Clamp(clampx, -40.0f, 60.0f);

        q.eulerAngles = new Vector3(clampx, q.eulerAngles.y, q.eulerAngles.z);
        follow.rotation = q;

    }
}
