using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshDamageHPS : MonoBehaviourPun
{
    public int hp = 5; //체력
    private GameObject Score;
    public GameObject BigBumb;

    private void Start()
    {
        Score = GameObject.Find("Score");
    }

    [PunRPC]
    public void RPCDamage(int attack) //만약 이 Damage함수를 호출하면 attack만큼 HP를 줄이겠다.
    {
        hp = hp - attack;
        if (hp <= 0)
        {
            BigBumb.SetActive(true);
            //승리 조건 score를 하나 증가시키고 이 gameobject를 파괴한다.
            Score.GetComponent<cshWinScore>().DiscountScore(this.gameObject);
            Destroy(gameObject);
            Debug.Log("send");
        }
    }
}
