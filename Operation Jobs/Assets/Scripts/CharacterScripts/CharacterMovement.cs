using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody rb;

    float xPos, zPos, yPos;
    [SerializeField] float speed;

    Vector3 direction;

    CharacterController player;

    [SerializeField] float gravity;
    [SerializeField] float jumpForce;
    
    bool jump, jumpPressed, isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask groundMask;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isInteracting)
        {
            TakeInputs();
            CheckGround();
            if (xPos != 0 || zPos != 0)
            {
                Rotate();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!player.isInteracting)
        {
            Move();
        }
    }

    void TakeInputs()
    {
        xPos = Input.GetAxis("Horizontal");
        zPos = Input.GetAxis("Vertical");

        jumpPressed = Input.GetKey(KeyCode.Space);
    }

    void Move()
    {
        if (jumpPressed)
        {
            jumpPressed = false;
            if (isGrounded)
            {
                jump = true;
                yPos = jumpForce;
            }
        }

        if (isGrounded && yPos <= 0f)
        {
            yPos = 0f;
            jump = false;
        }
        else
        {
            yPos -= gravity;
        }

        direction = (xPos * transform.right + zPos * transform.forward) * speed + yPos * transform.up;

        rb.velocity = direction * Time.fixedDeltaTime;
    }

    void Rotate()
    {
        transform.eulerAngles = new Vector3(0f, Camera.main.transform.parent.eulerAngles.y, 0f);
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
