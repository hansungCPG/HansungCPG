using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshMCPlayer : MonoBehaviourPun
{
    public GameObject[] Cam;

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine) //내가 아니면 = 다른 사용자들이면
        {
            Camera[] cameras;
            cameras = transform.gameObject.GetComponentsInChildren<Camera>();

            foreach (Camera c in cameras)
            {
                c.enabled = false;
            }
            //다른 사용자들에게 나의 첫번째 자식(= canvas)을 보지 못하게 false
            //transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
    }
}
