using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshARCameraRotate : MonoBehaviour
{
    [SerializeField] float m_Speed;
    public float rotSpeed = 0.3f;

    bool mouseState = false;

    bool dragState; // Joystick을 터치하고 있는지 여부를 확인

    public GameObject joystickArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
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

    }
}
