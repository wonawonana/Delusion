using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI;
    [SerializeField] private GameObject ENDUI;
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P)){
            if(PlayerController.isEnding==true)
                callEnding();
        //}

    }
    private void callEnding(){
        go_BaseUI.SetActive(true);
    }
    public void ClickSubtle(){
        go_BaseUI.SetActive(false);
        ENDUI.SetActive(true);
    }
}

