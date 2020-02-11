using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerBody;

    public float mouseSensitivity = 200f;
    float xRotation = 0;

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
        playerBody.Rotate(Vector3.up * mx);

        xRotation -= my;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        //rotate the camera up and down like a head
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}