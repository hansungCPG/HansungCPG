using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class cshMVRRaySpawn : MonoBehaviourPun
{
    public GameObject FPMuzzleFlash; //1인칭 총구 화염
    public Transform muzzle;        //실제 플레이어가 쏘는 총구 위치
    public AudioClip shootSound; //사격 시 재생될 Sound
    private int oriBullet; //초기 총알 값을 저장하기 위한 변수

    public G_FPAnimationScript fpAnim; //1인칭 (손&&총) 케릭터
    private AudioSource audioSource;
    private GameObject pcCanvas; //PC UI 조작하기 위해 가져오는 GameObject
    public Animator fpAnimator;
    public Animator tpAnimator;

    public float range = 1000f; //총기 사거리
    public cshUI isDie;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fpAnim = GetComponentInChildren<G_FPAnimationScript>();
        pcCanvas = GameObject.Find("MVRCanvas2");
        oriBullet = pcCanvas.GetComponent<cshUI>().bullet;
    }

    // Update is called once per frame
    void Update()
    { //조이스틱에서 A키를 누르면 슛팅함수 재생
        if (Input.GetButtonDown("XboxA") && pcCanvas.GetComponent<cshUI>().bullet > 0 && isDie.isDead == false)
        {
            Shoot();
            fpAnimator.CrossFadeInFixedTime("Shoot", 0.1f);
            tpAnimator.CrossFadeInFixedTime("Shoot", 0.1f);
            Debug.Log("Shoot");
        }
        if (Input.GetButtonDown("XboxX")) //조이스틱에서 X키를 누르면 재장전
        {
            fpAnimator.CrossFadeInFixedTime("Reload", 0.1f);
            tpAnimator.CrossFadeInFixedTime("Reload", 0.1f);
            pcCanvas.GetComponent<cshUI>().bullet = oriBullet;
            Debug.Log("xboxX");
        }
    }

    private void Shoot()
    {
        pcCanvas.GetComponent<cshUI>().PlayerBullet(); //쏠 때마다 총알 갯수 감소

        fpAnim.Fire();
        //총구 화염
        FPMuzzleFlash.GetComponent<ParticleSystem>().Play();
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
