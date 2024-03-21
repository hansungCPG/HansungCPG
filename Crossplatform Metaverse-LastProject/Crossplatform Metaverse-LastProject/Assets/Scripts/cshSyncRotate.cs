using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshSyncRotate : MonoBehaviourPun
{
    public Transform target; // 카메라
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        SyncRotate();
    }

    void SyncRotate()
    {
        Vector3 project = Vector3.Project(target.forward, Vector3.up);
        Vector3 look = target.forward - project;
        look = look.normalized;

        transform.LookAt(transform.position + look);
    }
}
