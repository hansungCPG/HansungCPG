using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Reload : MonoBehaviour
{
    public G_Gun weapon;

    private float reloadBegin = 0.4f; //0~1에서 0.6정도 트리거를 당겼을 때 재장전
    private float triggerFlex = 0f;

    private float reloadRate = 1.5f;
    private float reloadTimer = 0.0f;

    private AudioSource audioSource;
    public AudioClip reloadSound;

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
        if (hand == HandPosition.Left)
            triggerFlex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        else
            triggerFlex = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        if (reloadTimer < reloadRate)
            reloadTimer += Time.deltaTime;
    }

    //컬리더에 머물러있고 버튼 눌렀을때, 재장전
    private void OnTriggerStay(Collider other)
    {
        if(triggerFlex > reloadBegin && other.gameObject.tag == "ReloadTrigger" && reloadTimer > reloadRate)
        {
            audioSource.PlayOneShot(reloadSound);
            weapon.DoReload();
            reloadTimer = 0.0f;
        }
    }
}
