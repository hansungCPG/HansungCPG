using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshMobileVRPlayer : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            Camera[] cameras;
            cameras = transform.gameObject.GetComponentsInChildren<Camera>();
            foreach (Camera c in cameras)
            {
                c.enabled = false;
            }
        }
        else
        {   
            // GVR의 경우, GVREventSystem과 EventSystem이 충돌
            GameObject.Find("EventSystem").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false); // GvrReticlePointer
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false); //MobileVRCamera
            transform.GetChild(1).gameObject.SetActive(false); // GvrEventSystem
            transform.GetChild(2).gameObject.SetActive(false); // GvrEditorEmulator
            transform.GetChild(3).gameObject.SetActive(false); // Canvas
            transform.GetChild(4).GetChild(3).gameObject.SetActive(false); //MVRCanvas
            return;
        }
    }  
}
