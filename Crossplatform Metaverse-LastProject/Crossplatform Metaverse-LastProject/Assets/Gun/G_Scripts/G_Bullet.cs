using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Bullet : MonoBehaviour
{
    public GameObject bulletHit;
    private GameObject tempParticleEffect;
    //public GameObject bulletHole;

    public float speed = 50f;
    public float lifeTime = 5.0f;
    public int damage = 20;

    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            tempParticleEffect = Instantiate(bulletHit, transform.position, Quaternion.Euler(Vector3.up));
            tempParticleEffect.GetComponent<ParticleSystem>().Play();
            Destroy(tempParticleEffect, 3.0f);
        }
        //GameObject tempHole = Instantiate(bulletHole, transform.position, Quaternion.Euler(Vector3.up));
        Destroy(gameObject);
    }

    public void Shoot(Transform muzzle)
    {
        rigid.AddForce(muzzle.forward * speed);
    }
}
