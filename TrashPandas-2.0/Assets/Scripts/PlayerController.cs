using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;

    public float gravity = 9.81f;

    public float jumpheight = 100;

    public float airControl = 10;

    Vector3 moveDir;

    CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        //var input = new Vector3(horiz, 0, vert);

        Vector3 camIdealForward = Vector3.Cross(Camera.main.transform.right, Vector3.up);

        var input = transform.right * horiz + camIdealForward * vert;

        input *= moveSpeed;

        if (cc.isGrounded)
        {
            moveDir = input;

            if (Input.GetButton("Jump"))
            {
                moveDir.y = Mathf.Sqrt(2 * gravity * jumpheight);
            }
            else
            {
                moveDir.y = 0f;
            }
        }
        else
        {
            input.y = moveDir.y;

            moveDir = Vector3.Lerp(moveDir, input, airControl * Time.deltaTime);
        }


        moveDir.y -= gravity * Time.deltaTime;

        cc.Move(moveDir * Time.deltaTime);
    }
}
