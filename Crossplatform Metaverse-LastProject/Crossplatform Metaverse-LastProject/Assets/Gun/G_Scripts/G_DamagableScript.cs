using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_DamagableScript : MonoBehaviour
{
    public G_PCCharacter pc;
    public G_MobileCharacter mobile;
    public cshUI mobileVR;
    public G_VRPlayer oculus;
    public cshUI AR;

    private void Start()
    {
        pc = GetComponent<G_PCCharacter>();
        mobile = GetComponent<G_MobileCharacter>();
        mobileVR = GetComponent<cshUI>();
        oculus = GetComponent<G_VRPlayer>();
        AR = GetComponent<cshUI>();
    }

    public void TakeDamage(int damage)
    {
        if(pc)
        {
            pc.TakeDamage(damage);
        }
        if(mobile)
        {
            mobile.TakeDamage(damage);
        }
        if(mobileVR)
        {
            mobileVR.TakeDamage(damage);
        }
        if (oculus)
        {
            oculus.TakeDamage(damage);
        }
        if (AR)
        {
            AR.TakeDamage(damage);
        }
    }
}
