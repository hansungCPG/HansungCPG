using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class cshGameManager : MonoBehaviourPun // 점수와 게임 오버 여부 및 게임 UI를 관리하는 게임 매니저 스크립트
{
    public static cshGameManager instance // 외부에서 싱글톤 오브젝트를 가져올때 사용할 프로퍼티
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<cshGameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static cshGameManager m_instance; // 싱글톤이 할당될 static 변수

    public GameObject[] Players;
    public GameObject[] PlayersPos;

    /*
    public GameObject VRPlayerPrefab; // 생성할 VR 플레이어 캐릭터
    public GameObject ARPlayerPrefab; // 생성할 AR 플레이어 캐릭터

    public GameObject VRSpawnPosPrefab; // 생성할 VR 플레이어 캐릭터의 위치
    public GameObject ARSpawnPosPrefab; // 생성할 AR 플레이어 캐릭터의 위치
    */

    private int playerCnt = 0;

    public int userId = 1;

    Hashtable Player = new Hashtable();
    bool state = true;
    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        userId = GameObject.FindWithTag("UserId").GetComponent<cshSelectUser>().userId;
        //userId = GameObject.Find("UserId").GetComponent<cshSelectUser>().userId;
        //PC Editor 확인모드//////
        //Photon.Realtime.Player[] plys = PhotonNetwork.PlayerListOthers;
        //playerCnt = PhotonNetwork.CountOfPlayers - 1;
        /////////////////////////
        ///
        //if (playerCnt == 0)
        //if (Application.platform != RuntimePlatform.Android)
        Vector3 randomSpawnPos = PlayersPos[userId-1].transform.position;//Random.insideUnitSphere * 5f;
        PhotonNetwork.Instantiate(Players[userId-1].name, randomSpawnPos, Quaternion.identity);

        //생성됨과함께 게임메니저가 관리하기위하여 ArrayList에 등록
        /*
        if(userId == 1)
        {
            // 생성할 랜덤 위치 지정
            Vector3 randomSpawnPos = VRSpawnPosPrefab.transform.position;//Random.insideUnitSphere * 5f;

            // 네트워크상의 모든 클라이언트에서 생성 실행  
            // 해당 게임 오브젝트의 주도권은 생성 메서드를 직접 실행한 클라이언트에 있음
            PhotonNetwork.Instantiate(VRPlayerPrefab.name, randomSpawnPos, Quaternion.identity);
            //playerCnt++;
        }
        else
        {
            Vector3 randomSpawnPos = ARSpawnPosPrefab.transform.position;//Random.insideUnitSphere * 5f;
            PhotonNetwork.Instantiate(ARPlayerPrefab.name, randomSpawnPos, Quaternion.identity);
        }
        */
        //else
        /*
        if (Application.platform == RuntimePlatform.Android) // Android 디바이스 모드       
        {
            Vector3 randomSpawnPos = ARSpawnPosPrefab.transform.position;//Random.insideUnitSphere * 5f;
            PhotonNetwork.Instantiate(ARPlayerPrefab.name, randomSpawnPos, Quaternion.identity);
        }
        else
        {
                // 생성할 랜덤 위치 지정
                Vector3 randomSpawnPos = VRSpawnPosPrefab.transform.position;//Random.insideUnitSphere * 5f;

                // 네트워크상의 모든 클라이언트에서 생성 실행  
                // 해당 게임 오브젝트의 주도권은 생성 메서드를 직접 실행한 클라이언트에 있음
                PhotonNetwork.Instantiate(VRPlayerPrefab.name, randomSpawnPos, Quaternion.identity);
                //playerCnt++;

        }
        */
        //if (Application.platform == RuntimePlatform.Android) // Android 디바이스 모드       
    }

    void PlayerViewSystem()
    {

    }
}
