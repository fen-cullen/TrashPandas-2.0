using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //edit
    
    public Transform playerBody;

    float distance;
    public float distoffset = 10;

    public float rotoffset = 45;

    public float mouseSensitivity = 200f;


    // Start is called before the first frame update
    void Start()
    {
        if (playerBody == null)
        {
            playerBody = transform.parent.transform;
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //rotate the player left and right so that you can continue to move forward
        //playerBody.Rotate(Vector3.up * mx);

        //xRotation -= my;
        //xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        ////rotate the camera up and down like a head
        //transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        distance = Vector3.Distance(transform.position, playerBody.position);


   

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -1 * distoffset, distoffset), transform.position.z);


        transform.Translate(Vector3.up * my);




        transform.LookAt(playerBody);


    }
}
