using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshFire : MonoBehaviourPun
{
    private Transform rocketShoot;
    private float shootSpeed = 500.0f;
    public GameObject bombParticle;
    public float destroyTime = 10.0f;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false); //처음 파티클 false
        Player = GameObject.Find("ARBody"); //로켓 쐈을 때 플레이어의 rotation에 맞춰 로켓의 방향 전환을 위해 ARBody 가져오기
        rocketShoot = GetComponent<Transform>(); //현재 위치 가져오기
        GetComponent<Rigidbody>().AddForce(rocketShoot.forward * shootSpeed); //일정한 힘들 가해서 로켓 발사 구현
        transform.localEulerAngles = new Vector3(0, 90 + Player.transform.localEulerAngles.y, 0); //로켓의 방향 틀어짐 현상을 방지

        InvokeRepeating("OnTriggerEnter", 1.0f, Time.deltaTime);
    }

    private void Update()
    {
        //일정 시간이 지난 후 로켓 파괴
        Destroy(this.gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //적과 로켓이 부딛혔을 경우 RPC함수를 호출하기 위해 선언
        PhotonView pv = collision.transform.GetComponent<PhotonView>();

        GameObject bombObject;
        bombObject = PhotonNetwork.Instantiate("PbombParticle", this.transform.position, this.transform.rotation);
        bombObject.GetComponent<ParticleSystem>().Play();

        if (pv != null && pv.tag !="Player") //부딪힌 collision이 PhotonView컴포넌트를 가지고 있다면?
        {
            pv.RPC("RPCDamage", RpcTarget.All, 2); //HP-2

            Destroy(gameObject, 10f);
            Destroy(bombObject, 10f);
        }
    }
}
