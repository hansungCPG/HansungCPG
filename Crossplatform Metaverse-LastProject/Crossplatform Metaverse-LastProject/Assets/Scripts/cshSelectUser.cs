using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cshSelectUser : MonoBehaviour
{
    public int userId = 1; // 1:PC, 2:Mobile, 3:PCVR, 4:MobileVR, 5:AR
    public Button[] btnUserList;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        btnUserList[userId-1].interactable = false;
        btnUserList[userId-1].colors = setColor(btnUserList[userId - 1], Color.cyan);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPlayer(int id)
    {
        userId = id;
        for (int i = 0; i < btnUserList.Length; i++)
        {
            btnUserList[i].interactable = true;
            btnUserList[i].colors = setColor(btnUserList[i], new Color(200/255f,200 / 255f, 200 / 255f, 128 / 255f));
        }
            
        btnUserList[id-1].interactable = false;
        btnUserList[id-1].colors = setColor(btnUserList[id - 1], Color.cyan);
    }

    private ColorBlock setColor(Button b, Color c)
    {
        ColorBlock cb = b.colors;
        cb.disabledColor = c;
        return cb;
    }
}
