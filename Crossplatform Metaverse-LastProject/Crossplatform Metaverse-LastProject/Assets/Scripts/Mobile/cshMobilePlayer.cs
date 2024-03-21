using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshMobilePlayer : MonoBehaviourPun
{
    public bool State = true; // true: 1인칭, false: 3인칭
    public GameObject[] Cam;

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

            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            //자식 중 첫번째를 가져와서 false로 만든다 = Canvas
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            return;
        }
    }

    public void OnClickViewButton()
    {
        if (State) // 1인칭
        {
            GetComponentInChildren<cshMobileRaySpawn>().isFPMuzzle = State;
            State = false;
            Cam[0].SetActive(false);
            Cam[1].SetActive(true);
        }
        else // 3인칭
        {
            State = true;
            Cam[0].SetActive(true);
            Cam[1].SetActive(false);
        }
    }
}
