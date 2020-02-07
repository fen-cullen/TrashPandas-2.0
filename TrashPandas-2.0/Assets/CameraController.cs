using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset;

    void Start()
    {

    }

    void LateUpdate()
    {
        SetCameraOffset();

        SetCameraRotation();

    }

    void SetCameraOffset()
    {
        Vector3 cross = Vector3.Cross(Vector3.up, player.transform.right).normalized * offset.z;
        transform.position = player.transform.position + cross + new Vector3(0, offset.y, 0);
        transform.LookAt(player.transform.position + new Vector3(0, offset.y, 0));
    }

    void SetCameraRotation()
    {

    }
}