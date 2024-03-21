using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class cshJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image imgBG;
    private Image imgJoystick;
    private Vector3 vInputVector;

    public float time = 0.0f; //적당 시간 흐른 뒤 속도를 높여주기위한 시간측정에 이용할 변수
    public bool onDrag; //현재 마우스가 조이패드를 드래그중인지 확인하기위한 변수


    // Start is called before the first frame update
    void Start()
    {
        imgBG = GetComponent<Image>();
        imgJoystick = transform.GetChild(0).GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
     
        Debug.Log("Joystick >>> OnDrag()");
        Vector2 pos;

        //배경 영역에 터치가 발생할 때
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgBG.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        { 
            //Debug.Log(imgBG.rectTransform.sizeDelta);
            //터치된 로컬 좌표값을 pos에 저장
            //배경 이미지의 size로 나누어 pos.x: -1~1, pos.y: -1~1 으로 변환
            pos.x = (pos.x / imgBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / imgBG.rectTransform.sizeDelta.y);

            vInputVector = new Vector3(pos.x, pos.y, 0);
            vInputVector = (vInputVector.magnitude > 1.0f) ? vInputVector.normalized : vInputVector;

            //Joystick Image 움직임
            imgJoystick.rectTransform.anchoredPosition = new Vector3(vInputVector.x * (imgBG.rectTransform.sizeDelta.x / 2),
                                                                    vInputVector.y * (imgBG.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        onDrag = true; //마우스가 조이패드를 눌렀을 때 true
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        time = 0.0f;
        vInputVector = Vector3.zero;
        imgJoystick.rectTransform.anchoredPosition = Vector3.zero;
        onDrag = false; //조이패드에 마우스클릭을 땟을 때 false

    }
    public float GetHorizontalValue()
    {
        return vInputVector.x;
    }
    public float GetVerticalValue()
    {
        return vInputVector.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(onDrag) //조이패드에 마우스클릭이 눌린 상태라면
            time += Time.deltaTime; //지난 시간을 측정해서 저장
    }

}