using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cshButton : MonoBehaviour
{
    public Button btnJump;
    public Button btnView;
    public cshMobileFPMove sPlayer;

    // Start is called before the first frame update
    void Start()
    {
        btnJump.gameObject.SetActive(true);
        //btnJump.onClick.RemoveAllListeners();
        //btnJump.onClick.AddListener(OnClickJumpButton);
        btnView.gameObject.SetActive(true);
        //btnView.onClick.RemoveAllListeners();
        //btnView.onClick.AddListener(OnClickViewButton);
    }

    private void OnClickJumpButton()
    {
        sPlayer.OnVirtualPadJump();
    }

    private void OnClickViewButton()
    {
        //sPlayer.OnVirtualPadView();
    }

}