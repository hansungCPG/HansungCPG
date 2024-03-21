using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshCreateEnemyFactory : MonoBehaviourPun
{
    private float defaultTime = 0.0f;
    public float createTime = 5.0f;
    public cshWinScore facScore;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("Enemy", this.transform.position, this.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        CreateEnemy();
        DestroyEnemy();
    }
    void CreateEnemy() //적 생성 함수
    {
        defaultTime += Time.deltaTime;
        if (defaultTime >= createTime) //적을 생성할 시간만큼 흐르면 
        {
            //적 생성
            PhotonNetwork.Instantiate("Enemy", this.transform.position, this.transform.rotation);
            defaultTime = 0.0f;
            Debug.Log("createEnemy");
        }
    }

    void DestroyEnemy()
    {
        if (facScore.facCount == facScore.facOriCount)
            this.gameObject.SetActive(false);
    }
}
