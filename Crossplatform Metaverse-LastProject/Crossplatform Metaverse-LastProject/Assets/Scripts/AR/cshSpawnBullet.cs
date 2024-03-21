using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class cshSpawnBullet : MonoBehaviourPun
{
    public Transform BulletRight;
    public Transform BulletLeft;
    public Transform BulletFirePosRight;
    public Transform BulletFirePosLeft;
    public AudioClip shootSound; //로켓 발사 시 재생될 Sound

    private int oriBullet;
    private GameObject arCanvas;
    private AudioSource audioSource;

    private void Start()
    {
        arCanvas = GameObject.Find("ARCanvas");
        oriBullet = arCanvas.GetComponent<cshUI>().bullet;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void CreateBullet()
    {
        PhotonNetwork.Instantiate("PRocket", BulletFirePosRight.position, BulletFirePosRight.rotation);
        PhotonNetwork.Instantiate("PRocket", BulletFirePosLeft.position, BulletFirePosLeft.rotation);

        arCanvas.GetComponent<cshUI>().PlayerBullet(); 
        //발사 효과음
        audioSource.PlayOneShot(shootSound);
    }

    public void ReloadBullet()
    {
        arCanvas.GetComponent<cshUI>().bullet = oriBullet;
    }
}
