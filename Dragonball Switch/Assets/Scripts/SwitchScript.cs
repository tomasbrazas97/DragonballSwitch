using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{

    //Game objects
    public GameObject char1, char2;
    public CameraController MainCamera;
    public Vector3 tempPosition;

    //Which character is displayed
    int charDisplayed = 1;

    //Initialization 
    void Start()
    {

        //Enable char 1 and hide char 2
        char1.gameObject.SetActive(true);
        char2.gameObject.SetActive(false);

        MainCamera = FindObjectOfType<CameraController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCharacter();
        }
    }
    //Switch method
     public void SwitchCharacter()
    {

        switch (charDisplayed)
        {

            // if the char 1 is displayed
            case 1:
                tempPosition= char1.transform.position;
                char1.transform.position = char2.transform.position;
                char2.transform.position = tempPosition;
                //switch to char 2
                charDisplayed = 2;  

                // hide 1 and activate 2
                char1.gameObject.SetActive(false);
                char2.gameObject.SetActive(true);
                break;

            // if the char 1 is displayed
            case 2:
                tempPosition = char2.transform.position;
                char2.transform.position = char1.transform.position;
                char1.transform.position = tempPosition;
                //switch to char 1
                charDisplayed = 1;

                // hide 2 and activate 1
                char1.gameObject.SetActive(true);
                char2.gameObject.SetActive(false);
                break;
        }

    }
}
