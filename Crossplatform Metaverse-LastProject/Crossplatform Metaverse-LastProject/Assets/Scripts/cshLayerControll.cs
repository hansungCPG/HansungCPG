using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class cshLayerControll : MonoBehaviour
{
    public GameObject ThirdBody;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInParent<PhotonView>().IsMine) {
            ThirdBody.GetComponent<cshChangeLayer>().ChangeLayer("mybody");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
