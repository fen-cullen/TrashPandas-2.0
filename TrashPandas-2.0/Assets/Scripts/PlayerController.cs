using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 500;
    public float rotSpeed = 100;
    public float jump = 500;
    private bool grounded = false;

    public float playerMass = 1;
    public float gravity = 1;
    public float frictionCo = 1;
    public float slopeAngle = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool jump = Input.GetButton("Jump");

        //rb.AddRelativeForce(rb. * moveSpeed * Time.deltaTime);

        if (rb.velocity != Vector3.zero)
        {

        }
    }

    private float calcFrictionForce()
    {
        return frictionCo * calcNormalForce();
    }

    private float calcNormalForce()
    {
        return playerMass * gravity * Mathf.Cos(slopeAngle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag.Equals("Floor"))
        {
            Debug.Log("Collision with floor");
            grounded = true;
        }
    }
}
