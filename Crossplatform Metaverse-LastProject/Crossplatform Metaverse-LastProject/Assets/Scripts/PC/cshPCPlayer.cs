using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshPCPlayer : MonoBehaviourPun
{
    bool State = true; // true: 1인칭, false: 3인칭
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
            transform.GetChild(1).gameObject.SetActive(false);
            GetComponentInChildren<Camera>().cullingMask = ~(1 << LayerMask.NameToLayer("FP_FirstPerson"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            return;
        }

        //1인칭, 3인칭 시점변환
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (State) // 1인칭
            {
                GetComponentInChildren<cshPCRaySpawn>().isFPMuzzle = State;
                State = false;
                Cam[0].SetActive(false);
                Cam[1].SetActive(true);
                gameObject.GetComponentInChildren<cshSyncRotate>().enabled = false;
                //gameObject.GetComponentInChildren<cshSyncRotate>().target = Cam[1].GetComponent<Transform>();
            }
            else // 3인칭
            {
                State = true;
                Cam[0].SetActive(true);
                Cam[1].SetActive(false);
                gameObject.GetComponentInChildren<cshSyncRotate>().enabled = true;
            }
        }
    }
}