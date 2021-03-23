using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float groundSpeed = 7f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float dashSpeed = 100f;
    [SerializeField] float dashDuration = 0.01f;


    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    private Rigidbody rb;

    private Vector3 movePos;
    private bool isGrounded;
    private bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = groundSpeed;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(!isDashing)
            GetMovementInput();
        Jump();
        Dash();
        
        if(isDashing)
            CheckBounds();

    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movePos * speed * Time.fixedDeltaTime);
    }

    private void GetMovementInput()
    {
        Vector3 inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");

        movePos = transform.right * inputs.x + transform.forward * inputs.z;
    }

    private void Jump()
    {
         if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravity), ForceMode.VelocityChange);
            //rb.velocity = New Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Dash"))
        {
            isDashing = true;
            rb.drag = 8f;
            rb.AddForce(movePos.normalized * dashSpeed, ForceMode.VelocityChange);
            StartCoroutine(DashTimer());
        }
    }

    IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.drag = 1f;
        rb.velocity = Vector3.zero;
    }

    private void CheckBounds()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, movePos, 8f, groundMask))
        {
            movePos = Vector3.zero;
        }
    }
}
