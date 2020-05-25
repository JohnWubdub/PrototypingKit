using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INTENT: Basic FPS camera mouse controls 
//USAGE: Place on the camera attached to the player

//Created by @srgovindan
public class CameraLook : MonoBehaviour
{
    private GameObject player;
    public float rotationSpeed = 5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //get mouse input
        float horizontalSpeed = Input.GetAxis("Mouse X") * rotationSpeed;
        float verticalSpeed = Input.GetAxis("Mouse Y") * rotationSpeed;

        //rotate camera using input
        transform.Rotate(0f,horizontalSpeed,0f);
        transform.Rotate(-verticalSpeed,0f,0f);
        
        //lock the Z axis rotation 
        transform.eulerAngles= new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,0f);
    }
}
