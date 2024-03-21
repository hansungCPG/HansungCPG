using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_Gun : MonoBehaviour
{
    public GameObject crosshair;    //에임 크로스헤어
    private Vector3 originScale = Vector3.one * 0.02f;  //에임 크로스헤어의 원래 크기

    public GameObject muzzleFlashPrefab;    //총구 화염
    public GameObject hitParticle;          //피격 시 효과
    public GameObject bulletHole;           //벽에 남는 총알 흔적
    public GameObject bulletPrefab;         //총알(사용하지 않을 예정)
    public Transform muzzle;                //총구 위치

    public ParticleSystem avatarMuzzleFlash;          //아바타가 사용할 총구

    public AudioClip shootSound;            //발사 효과음
    public AudioClip reloadSound;           //재장전 소리

    public float fireRate = 1f;     //공속
    public float range = 100f;      //총기 사거리
    public int bulletsPerMag = 100;  //최대 총알
    public int currentBullets = 80;    //현재 총알

    private float fireTimer;        //사격 후 시간 측정용

    private float fireBegin = 0.6f; //0~1에서 0.6정도 트리거를 당겼을 때 사격
    private float triggerFlex = 0f;

    private AudioSource audioSource;

    public enum ShootMode { Auto, Semi }
    public ShootMode shootMode = ShootMode.Auto;    //사격모드
    private bool isTriggerOut = false;              //트리거에서 손을 뗐는가

    public enum HandPosition
    {
        Left,
        Right,
    }

    public HandPosition hand;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //왼손 총이면 왼손 발사 버튼, 오른손 총이면 오른손 발사 버튼을 입력받는다
        if (hand == HandPosition.Left)
            triggerFlex = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        else
            triggerFlex = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        //if (hand == HandPosition.Left)
        //{
        //    
        //}
        //else
        //{
        //    if (OVRInput.Get(OVRInput.Button.One))
        //    {
        //        if (shootMode == ShootMode.Auto)
        //            shootMode = ShootMode.Semi;
        //        else
        //            shootMode = ShootMode.Auto;
        //    }
        //}

        if (OVRInput.Get(OVRInput.Button.One))
        {
            if (shootMode == ShootMode.Auto)
                shootMode = ShootMode.Semi;
            else
                shootMode = ShootMode.Auto;
        }


        //발사 버튼을 일정치 이상 누르면 발사한다
        if (triggerFlex > fireBegin && !isTriggerOut)
        {
            if(shootMode == ShootMode.Semi)
                isTriggerOut = true;
            Shoot();
        }
        else if (triggerFlex > 0.3f)
            isTriggerOut = false;

        DrawCrosshair();
        
        if(fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }
    }

    private void Shoot()
    {
        //공속을 만족하지 않았거나, 총알이 없으면 발사불가
        if (fireTimer < fireRate || currentBullets <= 0) return;

        if(muzzleFlashPrefab)
        {
            //총구 화염
            muzzleFlashPrefab.GetComponent<ParticleSystem>().Play();
            avatarMuzzleFlash.Play();
        }
        //발사 효과음
        audioSource.PlayOneShot(shootSound);

        //레이저 쏘아 피격 계산
        //몬스터 피격 시 코드
        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, muzzle.transform.forward, out hit, range))
        {
            GameObject madedBulletHole;
            GameObject hitParticleEffect;

            //("충돌되는 모든 object에 생성되는 particle 이름", 레이에 맞은 위치 값, 회전 값)
            hitParticleEffect = PhotonNetwork.Instantiate("PhitParticle", hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
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
            }
            Destroy(hitParticleEffect, 1f);
        }

        currentBullets -= 1;
        fireTimer = 0.0f;
    }

    //총구가 향하는 곳에 크로스헤어 그리기
    public void DrawCrosshair()
    {
        //총구 위치에서 레이저를 발사한다
        Ray ray = new Ray(muzzle.position, muzzle.forward);

        Plane plane = new Plane(Vector3.up, 0);
        float distance = 0;

        //레이저가 닿은 위치에 수직방향인 plane을 위치시킨다
        if(plane.Raycast(ray, out distance))
        {
            //그 plane에 크로스헤어를 위치시킨다
            crosshair.transform.position = ray.GetPoint(distance);
            crosshair.transform.forward = -Camera.main.transform.forward;
            crosshair.transform.localScale = originScale * Mathf.Max(1, distance);
        }
        else
        {
            //닿은 물체가 없다면 공중에 그린다
            crosshair.transform.position = ray.origin + ray.direction * 100;
            crosshair.transform.forward = -Camera.main.transform.forward;
            distance = (crosshair.transform.position - ray.origin).magnitude;
            crosshair.transform.localScale = originScale * Mathf.Max(1, distance);
        }
    }

    public void DoReload()
    {
        currentBullets = 30;
    }
}
