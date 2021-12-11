using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] private GameObject go_BaseUI;

    // Update is called once per frame\\\

    void Start()
    {
        callEnding();
    }

    private void callEnding()
    {
        go_BaseUI.SetActive(true);
    }
    public void ClickSubtle()
    {
        go_BaseUI.SetActive(false);
        
    }
}

