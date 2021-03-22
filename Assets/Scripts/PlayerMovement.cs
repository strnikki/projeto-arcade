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
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        Move(isGrounded);
        Jump(isGrounded);
    }

    private void Move(bool isGrounded)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move = Vector3.ClampMagnitude(move, 1f);

        if(!isGrounded)
        {
            // For higher speeds while in air, remove this condition
            if(controller.velocity.magnitude < 7)
            {
            controller.Move(move * airSpeed * Time.deltaTime);
            }
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }
    }

    private void Jump(bool isgrounded)
    {
        if(Input.GetButton("Jump") && isGrounded)
        {
            // Mantain ground momentum
            velocity = controller.velocity;

            // v = sqrt(h * -2 * g) ??
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        // Gravity update
        if(isGrounded && velocity.y < 0)
        {
            velocity = Vector3.zero;
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        
        controller.Move(velocity * Time.deltaTime);
    }
}
