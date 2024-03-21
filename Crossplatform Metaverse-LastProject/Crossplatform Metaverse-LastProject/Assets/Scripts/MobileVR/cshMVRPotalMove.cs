using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cshMVRPotalMove : MonoBehaviour
{
    public Image LoadingBar;
    public int spaceCount = 4;
    private int i = 0;
    private bool IsOn; //gaze가 차오르고 있는지 확인 
    private bool IsFill; //gaze = 1 즉 다 찬 상태인지 확인
    private float barTime = 0.0f;

    public GameObject MVRuser;
    private GameObject MVRposition;
    //public ParticleSystem potalParticle;
    //bool setParticle = false; //이동을 할 때 한번 불러올 파티클을 검사할 변수

    public Transform[] potalList;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("LoadingBar") != null)
        {
            LoadingBar = GameObject.Find("LoadingBar").GetComponent<Image>();
            LoadingBar.fillAmount = 0;
        }

        MVRposition = GameObject.Find("MVRPosition");
        //Debug.Log(MVRposition);

        if (MVRposition != null)
        {
            for (int j = 0; j < potalList.Length; j++)
            {
                potalList[j] = MVRposition.transform.GetChild(j).gameObject.transform;
                Debug.Log(potalList[j]);
            }
        }
        else
            Debug.Log("no");

        IsOn = false;
        IsFill = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; //Ray에 맞은 게임오브젝트의 정보를 받아올 변수

        if (IsOn)
        {
            if (barTime <= 3.0f)
            {
                barTime += Time.deltaTime;
            }

            LoadingBar.fillAmount = barTime / 3.0f;

            if (LoadingBar.fillAmount == 1 && IsFill == false)
            {
                IsFill = true;

                if (Physics.Raycast(this.transform.position, this.transform.forward, out hit))
                {
                    //왼쪽 포탈인지 오른쪽 포탈인지에 따라 이동 i값 변경
                    if (hit.collider.tag == "LeftPotal")
                    {
                        i--;
                        if (i <= 0) i = potalList.Length-1;
                    }
                    else if (hit.collider.tag == "RightPotal")
                    {
                        i++;
                        if (i > potalList.Length-1) i = 0;
                    }
                    Debug.Log(hit.collider.tag);
                    Debug.Log(i);
                }

                MVRuser.transform.position = potalList[i].transform.position;
                MVRuser.transform.rotation = potalList[i].transform.rotation;
            }
        }
    }

    public void SetGazedAt(bool gazedAt)
    {
        IsOn = gazedAt;
        barTime = 0.0f;
        if (gazedAt)
        {
            //potalParticle.Play(true);
            LoadingBar = GameObject.Find("LoadingBar").GetComponent<Image>();
            Debug.Log("In");
        }
        else
        {
            //potalParticle.Play(false);
            Debug.Log("Out");
            LoadingBar.fillAmount = 0;

            IsFill = false;
        }
    }
}
