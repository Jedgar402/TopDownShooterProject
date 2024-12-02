using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    public Camera topViewCam;
    public Camera thirdPersonCam;

    private void Start()
    {
        //Start with topViewCam active and thirdPersonCam inactive
        topViewCam.gameObject.SetActive(true);
        thirdPersonCam.gameObject.SetActive(false);
    }

    void Update()
    {
        //Switch cameras when the player presses the spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        //Toggle the camera visibility
        topViewCam.gameObject.SetActive(!topViewCam.gameObject.activeSelf);
        thirdPersonCam.gameObject.SetActive(!thirdPersonCam.gameObject.activeSelf);
    }
}
