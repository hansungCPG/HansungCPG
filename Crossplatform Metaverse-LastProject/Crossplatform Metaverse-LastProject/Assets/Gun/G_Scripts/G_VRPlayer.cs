using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_VRPlayer : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;
    public bool isDead;

    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        character.GetComponent<G_VRRig>().enabled = false;
        character.GetComponent<Animator>().SetBool("isMoving", false);
        character.GetComponent<Animator>().SetBool("isDead", true);
        GetComponent<OVRPlayerController>().enabled = false;
    }
}
