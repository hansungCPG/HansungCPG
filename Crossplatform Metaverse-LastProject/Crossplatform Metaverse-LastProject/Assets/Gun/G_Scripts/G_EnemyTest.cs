using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_EnemyTest : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
