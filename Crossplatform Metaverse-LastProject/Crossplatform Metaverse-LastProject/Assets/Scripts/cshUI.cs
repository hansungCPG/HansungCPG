//UIManager(싱글톤) 사용: UI요소들에 접근하여 통제, 제어 기능을 이 스크립트에 몰아 넣기 위함.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cshUI : MonoBehaviour
{
    public Text textHP;
    public Text textBullet;
    public int hp = 10;
    public int bullet = 30;
    private int oriHp;
    private int oriBullet;

    public GameObject msgDie;
    public bool isDead = false;
    public GameObject fpGameObject;
    public Animator tpAnimator;

    void Start()
    {
        oriHp = hp;
        oriBullet = bullet;
    }


    void Update()
    {
        textHP.text = "HP " + hp + "/" + oriHp;
        textBullet.text = "GUN " + bullet + "/" + oriBullet;
    }

    public void TakeDamage(int damage)
    {
        hp = hp - damage;
        if(hp <= 0)
        {
            msgDie.SetActive(true);
            isDead = true;
            fpGameObject.SetActive(false);
            tpAnimator.SetBool("isDead", true);
            Debug.Log("Die");
        }
    }

    public void PlayerBullet()
    {
        if (bullet <= 0) bullet = 0;
        else bullet--;
    }
}
