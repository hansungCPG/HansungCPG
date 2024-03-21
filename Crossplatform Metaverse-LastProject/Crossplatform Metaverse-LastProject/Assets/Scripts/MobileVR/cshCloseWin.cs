using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshCloseWin : MonoBehaviour
{
    private bool gameClose = false;
    private bool keyClose = false;
    private bool missionClose = false;
    int i = 0;

    public GameObject mvrGameInfo;
    public GameObject mvrKeyInfo;
    public GameObject mvrMissionInfo;

    public GameObject btnGameInfo;
    public GameObject btnKeyInfo;
    public GameObject btnMissionInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("XboxB") && i == 0) MvrGameOnOff();
        else if (Input.GetButtonDown("XboxB") && i == 1) MvrKeyOnOff();
        else if (Input.GetButtonDown("XboxB") && i == 2) MvrMissionOnOff();

        if (Input.GetButtonDown("XboxY"))
        {
            if (i == 0)
            {
                mvrGameInfo.gameObject.SetActive(false);
                btnGameInfo.gameObject.SetActive(false);
                mvrKeyInfo.gameObject.SetActive(true);
                btnKeyInfo.gameObject.SetActive(true);
            }
            else if (i == 1)
            {
                mvrKeyInfo.gameObject.SetActive(false);
                btnKeyInfo.gameObject.SetActive(false);
                mvrMissionInfo.gameObject.SetActive(true);
                btnMissionInfo.gameObject.SetActive(true);
            }
            else if (i == 2)
            {
                mvrMissionInfo.gameObject.SetActive(false);
                btnMissionInfo.gameObject.SetActive(false);
                btnGameInfo.gameObject.SetActive(true);
                mvrGameInfo.gameObject.SetActive(true);
            }
            
            if (i >= 2) i = 0;
            else i++;
        }
    }

    void MvrGameOnOff()
    {
        if (gameClose == false)
        {
            mvrGameInfo.gameObject.SetActive(false);
            gameClose = true;
            Debug.Log("isClose true");
        }
        else if (gameClose == true)
        {
            mvrGameInfo.gameObject.SetActive(true);
            gameClose = false;
            Debug.Log("isClose false");
        }
    }

    void MvrKeyOnOff()
    {
        if (keyClose == false)
        {
            mvrKeyInfo.gameObject.SetActive(false);
            keyClose = true;
            Debug.Log("isClose true");
        }
        else if (keyClose == true)
        {
            mvrKeyInfo.gameObject.SetActive(true);
            keyClose = false;
            Debug.Log("isClose false");
        }
    }

    void MvrMissionOnOff()
    {
        if (missionClose == false)
        {
            mvrMissionInfo.gameObject.SetActive(false);
            missionClose = true;
            Debug.Log("isClose true");
        }
        else if (missionClose == true)
        {
            mvrMissionInfo.gameObject.SetActive(true);
            missionClose = false;
            Debug.Log("isClose false");
        }
    }
}
