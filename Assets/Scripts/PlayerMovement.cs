using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed = 7f;
    [SerializeField] float airSpeed = 4f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float jumpHeight = 3f;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    private CharacterController controller;

    private Vector3 velocity;
    private Vector3 moveVector;
    private bool isGrounded;
    private bool hasDoubleJump = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        Move();
        Jump();
        DoubleJump();
        UpdateGravity();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveVector = transform.right * x + transform.forward * z;
        moveVector = Vector3.ClampMagnitude(moveVector, 1f);

        if(!isGrounded)
        {
            //controller.Move(move * airSpeed * Time.deltaTime);
            Vector3 newVelocity = velocity + moveVector * 2 * speed * Time.deltaTime;
            if(newVelocity.magnitude < 7f)
            {
                velocity = newVelocity;
            }
        }
        else
        {
            controller.Move(moveVector * speed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            // Mantain ground momentum
            velocity = moveVector * speed;

            // v = sqrt(h * -2 * g) ??
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void DoubleJump()
    {
        if(isGrounded || !hasDoubleJump)
            return;

        if(Input.GetButtonDown("Jump"))
        {
            hasDoubleJump = false;

            if(moveVector.magnitude > 0f)
                velocity = moveVector * speed;
            else
                velocity = controller.velocity;

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void UpdateGravity()
    {
        if(isGrounded && velocity.y < 0)
        {
            velocity = Vector3.zero;
            velocity.y = -2f;
            hasDoubleJump = true;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        
        controller.Move(velocity * Time.deltaTime);
    }
}
