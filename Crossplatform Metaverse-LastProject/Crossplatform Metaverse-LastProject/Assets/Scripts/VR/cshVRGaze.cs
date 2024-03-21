using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cshVRGaze : MonoBehaviour
{
    public Image LoadingBar;
    public Image GazePointer;
    public Camera RayCamera;
    private bool IsOn;
    private float barTime = 0.0f;
    void Start()
    {
        IsOn = false;
        LoadingBar.fillAmount = 0;

    }

    void Update()
    {
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("GazeObject");  // Player 레이어만 충돌 체크함
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(RayCamera.transform.position, RayCamera.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(RayCamera.transform.position, RayCamera.transform.forward * hit.distance, Color.yellow);
            IsOn = true;

            /*
            Vector3 p = RayCamera.ScreenToWorldPoint(hit.transform.position);
            LoadingBar.transform.localPosition = new Vector3(p.x, p.y, p.z);
            */
            GazePointer.enabled = false;
        }
        else
        {
            Debug.DrawRay(RayCamera.transform.position, RayCamera.transform.forward * 1000, Color.red);
            IsOn = false;
            barTime = 0.0f;
            LoadingBar.fillAmount = 0;

            GazePointer.enabled = true;
        }


        if (IsOn)
        {
            if (barTime <= 3.0f)
            {
                barTime += Time.deltaTime;
            }
            LoadingBar.fillAmount = barTime / 3.0f;

            if(LoadingBar.fillAmount >= 1.0)
            {
                IsOn = false;
                barTime = 0.0f;
                LoadingBar.fillAmount = 0;
            }
        }
    }
}
