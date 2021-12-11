using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text keyText = null;
    private bool flag;
    public static bool Book;
    public static bool Book2;
    public static bool Book3;

    public static bool Room1Clear;
    public static bool Room2Clear;
    public static bool Room3Clear;


    // Start is called before the first frame update
    void Start()
    {
        keyText = GetComponent<Text>();
        flag = false;
        Book = false;
        Book2 = false;
        Book3 = false;


        Room1Clear = false;
        Room2Clear = false;
        Room3Clear = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.is5KeyPicked && flag == false)
        {
            keyText.text = "Got the key";
            flag = true;
        }

        if (Input.GetMouseButtonDown(0) && flag == true)
        {
            keyText.enabled=false;
        }

        if (PlayerController.isRoom1Book && Book == false)
        {
            keyText.text = "12/5 Sunny.\n My name is ___, and I am 23. My parents are dead, and I have a younger brother. My brother and I were kidnapped and trapped here. I have to go find my younger brother.";
            Book = true;
            Room1Clear = true;
        }

        if (Input.GetMouseButtonDown(0) && Book == true)
        {
            PlayerController.isRoom1Book = false;
            keyText.text = "";
            Book = false;
        }

        if (PlayerController.isRoom2Book && Book2 == false)
        {
            keyText.text = "12/15 Cloudy.\n My name is ...I have to go find my younger brother.";
            Book2 = true;
            Room2Clear = true;
        }

        if (Input.GetMouseButtonDown(0) && Book2 == true)
        {
            PlayerController.isRoom2Book = false;
            keyText.text = "";
            Book2 = false;
        }

        if (PlayerController.isRoom3Book && Book3 == false)
        {
            keyText.text = "12/20 Rain.\nMy name is...We have to escape.";
            Book3 = true;
            Room3Clear = true;
        }

        if (Input.GetMouseButtonDown(0) && Book3 == true)
        {
            PlayerController.isRoom3Book = false;
            keyText.text = "";
            Book3 = false;
        }


    }

}
