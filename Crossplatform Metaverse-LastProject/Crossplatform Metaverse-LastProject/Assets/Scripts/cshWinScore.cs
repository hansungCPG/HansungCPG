using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cshWinScore : MonoBehaviour
{
    public Text textFactory;
    public Text textCar;
    public Text textGarbage;
    public GameObject WinCanvas;

    public int facCount = 0;
    public int carCount = 0;
    public int garCount = 0;

    public int facOriCount = 0;
    public int carOriCount = 0;
    public int garOriCount = 0;

    public GameObject[] facList;
    public GameObject[] carList;
    public GameObject[] garList;

    private void Start()
    {
        facOriCount = facList.Length;
        carOriCount = carList.Length;
        garOriCount = garList.Length;
    }

    // Update is called once per frame
    void Update()
    {
        textFactory.text = facCount + "/" + facOriCount;
        textCar.text = carCount + "/" + carOriCount;
        textGarbage.text = garCount + "/" + garOriCount;

        if(facCount == facOriCount && carCount == carOriCount && garCount == garOriCount)
        {
            WinEvent();
        }
    }

    //score를 모두 채웠을 때(이겼을 때) 불릴 WinEvent함수
    void WinEvent()
    {
        WinCanvas.SetActive(true);
    }

    //태그로 부술 object 구분지어서 각 tag에 맞는 score 감소
    public void DiscountScore(GameObject DestroyObject)
    {
        if (DestroyObject.tag == "Factory") facCount++;
        else if (DestroyObject.tag == "Car") carCount++;
        else if (DestroyObject.tag == "Garbage") garCount++;
        else return;
    }
}
