using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum DashState
    {
        Ready, Dashing, Cooldown
    }


    public float moveSpeed = 10f;

    public float dashSpeed = 50f;

    public float dashDist = 50f;

    public float gravity = 9.81f;

    public float jumpheight = 100;

    public float airControl = 10;

    public float rotateSpeed = 1000;

    public int airJumps = 1;

    public DashState dashState;

    public float dashTimer;

    public float dashCooldown = 20f;

    Vector3 moveDir;

    CharacterController cc;

    RaccoonEffectPlayer audioPlayer;
    Animator animator;

    bool airdash = false;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        audioPlayer = GetComponent<RaccoonEffectPlayer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        //var input = new Vector3(horiz, 0, vert);

        Vector3 camIdealForward = Vector3.Cross(Camera.main.transform.right, Vector3.up);

        Vector3 camIdealRight = Vector3.Cross(Camera.main.transform.forward, Vector3.down);

        var input = camIdealRight * horiz + camIdealForward * vert;

        input *= moveSpeed;

        if (cc.isGrounded)
        {
            airJumps = 1;

            moveDir = input;

            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("jumped");
                moveDir.y = Mathf.Sqrt(2 * gravity * jumpheight);
                audioPlayer.PlayJumpSound();
                
            }
            else
            {
                moveDir.y = 0f;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && airJumps > 0)
            {
                animator.SetTrigger("jumped");
                moveDir.y = Mathf.Sqrt(2 * gravity * jumpheight);
                airJumps--;
                audioPlayer.PlayJumpSound();
            }

            input.y = moveDir.y;
            moveDir = Vector3.Lerp(moveDir, input, airControl * Time.deltaTime);
        }


        moveDir.y -= gravity * Time.deltaTime;

        cc.Move(moveDir * Time.deltaTime);

        Vector3 lookDir = cc.transform.position + new Vector3(moveDir.x, 0, moveDir.z);

        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), Time.deltaTime * rotateSpeed);

        cc.transform.LookAt(cc.transform.position + new Vector3(moveDir.x, 0, moveDir.z));

        //Handles the dash mechanic
        DashForwards(moveDir);

        animator.SetFloat("groundSpeed", new Vector2(moveDir.x, moveDir.z).magnitude);
        animator.SetBool("grounded", cc.isGrounded);
    }

    private void DashForwards(Vector3 moveDir)
    {
        switch (dashState)
        {
            case DashState.Ready:
                Debug.Log("Ready");
                var isDashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
                if (isDashKeyDown)
                {
                    airdash = !cc.isGrounded;
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;

                if(cc.velocity.magnitude != 0)
                {
                    Vector3 moveDir2 = cc.transform.position + moveDir;
                    cc.transform.position = Vector3.MoveTowards(cc.transform.position, new Vector3(moveDir2.x, cc.transform.position.y, moveDir2.z), dashSpeed);
                }

                if (dashTimer >= dashCooldown)
                {
                    dashTimer = dashCooldown;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                Debug.Log("Cooldown");
                if(!airdash)
                {
                    dashTimer -= Time.deltaTime;
                }
                else
                {
                    dashTimer -= Time.deltaTime;
                    if(cc.isGrounded)
                    {
                        dashTimer = 0;
                    }
                }
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }
}
