using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class G_BulletText : MonoBehaviour
{
    public G_Gun weapon;
    public TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        text.text = weapon.currentBullets.ToString();
    }
}
