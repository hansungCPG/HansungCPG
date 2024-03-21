using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FPAnimationScript : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Fire()
    {
        anim.CrossFadeInFixedTime("Fire", 0.1f);
    }

    public void Reload(bool ammoLeft)
    {
        if (ammoLeft)
            anim.SetTrigger("Reload");
        else
            anim.SetTrigger("ReloadNoAmmo");
    }
}
