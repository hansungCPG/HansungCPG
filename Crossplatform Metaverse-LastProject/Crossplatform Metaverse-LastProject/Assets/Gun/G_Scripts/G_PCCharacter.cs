using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/*
        플레이어의 체력과 총알들을 관리하는 스크립트
*/

public class G_PCCharacter : MonoBehaviour
{
    public Text textHP;
    public Text textBullet;

    public int health = 100;            //체력
    public int maxHealth = 100;         //최대체력
    public int currentBullets = 80;
    public int maxBullets = 80;
    public float shootRate = 0.1f;
    public float damage = 30f;

    public cshPCFPMove moveScript;
    public GameObject fpGameObject;

    public cshPCRaySpawn currentCamera;

    public Animator fpAnimator;
    public Animator tpAnimator;

    public AudioClip shootSound;
    public GameObject msgDie;

    private float shootTimer;
    private float reloadTimer;
    public bool isNoAmmo = false;
    public bool isReloading = false;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<cshPCFPMove>();
    }

    // Update is called once per frame
    void Update()
    {
        textHP.text = "HP " + health + "/" + maxHealth;
        textBullet.text = currentBullets + "/" + maxBullets;

        if (!isDead)
        {
            if (Input.GetButton("Fire1"))
            {
                if (currentBullets > 0)
                {
                    Shoot();
                }
                else if (!isReloading)
                {
                    Reload();
                }
            }

            if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            {
                Reload();
            }

            //재장전 모션을 위해 탄약이 남아있는지 검사
            if (currentBullets <= 0)
                isNoAmmo = true;
            else
                isNoAmmo = false;

            if (shootTimer < shootRate)
                shootTimer += Time.deltaTime;

            //재장전 모션 중 시점변환을하면 재장전이 완료되지않는 문제가 있어서,
            //재장전이 1.5초 이상 이루어지지 않았을 경우 bool값을 해제하는 임시방편이다.
            if (isReloading)
            {
                reloadTimer += Time.deltaTime;
                if (reloadTimer > 1.5f)
                {
                    isReloading = false;
                    reloadTimer = 0f;
                }
            }
        }
    }

    private void Shoot()
    {
        if (shootTimer < shootRate || isReloading) return;

        currentCamera.Shoot();
        currentBullets -= 1;
        fpAnimator.CrossFadeInFixedTime("Shoot", 0.1f);
        tpAnimator.CrossFadeInFixedTime("Shoot", 0.1f);

        shootTimer = 0.0f;
    }

    private void Reload()
    {
        isReloading = true;
        if(!isNoAmmo)
        {
            fpAnimator.CrossFadeInFixedTime("Reload", 0.1f);
            tpAnimator.CrossFadeInFixedTime("Reload", 0.1f);
        }
        else
        {
            fpAnimator.CrossFadeInFixedTime("ReloadNoAmmo", 0.1f);
            tpAnimator.CrossFadeInFixedTime("Reload", 0.1f);
        }
        
    }

    public void DoReload()
    {
        isReloading = false;
        currentBullets = maxBullets;
    }

    //호출되면 데미지 입는 함수
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    //죽는 함수
    private void Die()
    {
        msgDie.SetActive(true);
        isDead = true;
        moveScript.enabled = false;
        fpGameObject.SetActive(false);
        tpAnimator.SetBool("isDead", true);
        Debug.Log("Die");
    }
}
