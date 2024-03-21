using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshMobileRaySpawn : MonoBehaviourPun
{
    public GameObject FPMuzzleFlash; //1인칭 총구 화염
    public GameObject TPMuzzleFlash; //3인칭 총구 화염
    public Transform FPMuzzle; //1인칭 총구 위치
    public Transform TPMuzzle; //3인칭 총구 위치
    private Transform muzzle;        //실제 플레이어가 쏘는 총구 위치
    private GameObject RealMuzzleFlash; //실제 총구 화염 나가는 위치
    public AudioClip shootSound; //사격 시 재생될 Sound

    public G_FPAnimationScript fpAnim; //1인칭 (손&&총) 케릭터
    private AudioSource audioSource;
    public bool isFPMuzzle; //현재 1인칭인지 3인칭인지 구분해 주기위한 bool변수

    public float range = 100f; //총기 사거리

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fpAnim = GetComponentInChildren<G_FPAnimationScript>();
    }

    private void Update()
    {
        if (isFPMuzzle == true)
        {
            RealMuzzleFlash = FPMuzzleFlash;
            muzzle = FPMuzzle;
        }
        else if (isFPMuzzle == false)
        {
            RealMuzzleFlash = TPMuzzleFlash;
            muzzle = TPMuzzle;
        }
    }

    public void Shoot()
    {
        fpAnim.Fire();
        //총구 화염
        RealMuzzleFlash.GetComponent<ParticleSystem>().Play();
        //발사 효과음
        audioSource.PlayOneShot(shootSound);

        //레이저 쏘아 피격 계산
        //몬스터 피격 시 코드
        RaycastHit hit; //Ray생성 
        if (Physics.Raycast(muzzle.position, muzzle.transform.forward, out hit, range)) //충돌되는 object가 있는지 검사 
        {
            GameObject madedBulletHole;
            GameObject hitParticleEffect;
            //("충돌되는 모든 object에 생성되는 particle 이름", 레이에 맞은 위치 값, 회전 값)
            hitParticleEffect = PhotonNetwork.Instantiate("PhitParticle", hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
           Debug.Log("hitParticleEffect");
            hitParticleEffect.GetComponent<ParticleSystem>().Play();

            //Enemy또는 DestroyObject 피격 시 코드
            PhotonView pv = hit.transform.GetComponent<PhotonView>();
            if (pv != null && pv.tag != "Player") //hit이 PhotonView컴포넌트를 가지고 있다면? && 피격 당한 객체가 같은 player가 아니라면?
            {
                //나 지금 RPCDamage함수 호출했어! 라고 나를 포함한 모든 사용자들에게 알림, 인자는 1 전달
                pv.RPC("RPCDamage", RpcTarget.All, 1); //HP-1
            }
            else
            {
                //("몬스터가 아닌 대상에게 맞으면 총알자국을 생성할 파티클 이름", 레이에 맞은 위치 값, 회전 값)
                madedBulletHole = PhotonNetwork.Instantiate("PbulletHole", hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                madedBulletHole.GetComponent<ParticleSystem>().Play();
                Destroy(madedBulletHole, 5f);
                Debug.Log("madedBulletHole");
            }
            Debug.Log("hit");
            Destroy(hitParticleEffect, 1f);
        }
    }
}
