using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5.0f)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            timer = 0;
        }
    }
}
