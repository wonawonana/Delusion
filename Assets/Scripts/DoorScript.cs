using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool open = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = -180f;
    public float smoot = 2f;



    // Start is called before the first frame update
    void Start()
    {

    }

    public void ChangeDoorState()
    {
        open = !open;

        if (TextController.Room1Clear) TextController.Room1Clear = false;
        if (TextController.Room2Clear) TextController.Room2Clear = false;
        if (TextController.Room3Clear) TextController.Room3Clear = false;

    }


    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
        }

    }
}