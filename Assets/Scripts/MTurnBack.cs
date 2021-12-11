using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTurnBack : MonoBehaviour
{
    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.is5KeyPicked && flag == false)
        {
            transform.localEulerAngles = new Vector3(0, 340, 0);
            flag = true;
        }
       
    }
}
