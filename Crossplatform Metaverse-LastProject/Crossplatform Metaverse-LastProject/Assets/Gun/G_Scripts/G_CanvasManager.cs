using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class G_CanvasManager : MonoBehaviour
{
    public GameObject deadText;
    public Text currentBulletText;

    public void PCPlayerDead()
    {
        deadText.SetActive(true);
    }
}
