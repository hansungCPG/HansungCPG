//RPC(Remote Procedure Call)이용, 모든 사용자들에게 알릴 내용 처리
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshDamageHP : MonoBehaviourPun
{
    public int hp = 5; //체력

    [PunRPC]
    public void RPCDamage(int attack) //만약 이 Damage함수를 호출하면 attack만큼 HP를 줄이겠다.
    {
        hp = hp - attack;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
