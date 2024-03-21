using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class cshPointerEvent : MonoBehaviour
{
    public Image LoadingBar;
    private bool IsOn;
    private float barTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("LoadingBar") != null) { 
            LoadingBar = GameObject.Find("LoadingBar").GetComponent<Image>();
            LoadingBar.fillAmount = 0;
        }

        IsOn = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOn)
        {
            if (barTime <= 5.0f)
            {
                barTime += Time.deltaTime;
            }
            LoadingBar.fillAmount = barTime / 5.0f;
        }
    }

    public void SetGazedAt(bool gazedAt)
    {
        IsOn = gazedAt;
        barTime = 0.0f;
        if (gazedAt)
        {
            LoadingBar = GameObject.Find("LoadingBar").GetComponent<Image>();
            Debug.Log("In");
        }
        else
        {
            Debug.Log("Out");
            LoadingBar.fillAmount = 0;
        }
    }


}
