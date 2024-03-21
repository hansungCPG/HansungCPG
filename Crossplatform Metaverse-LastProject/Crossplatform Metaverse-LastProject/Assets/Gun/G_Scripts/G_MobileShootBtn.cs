using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class G_MobileShootBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isBtnDown = false;

    public G_MobileCharacter mobile;

    // Update is called once per frame
    void Update()
    {
        if(isBtnDown)
        {
            mobile.PressShootButton();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBtnDown = false;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isBtnDown = true;
    }
}
