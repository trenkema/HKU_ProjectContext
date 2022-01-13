using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float sprintSpeed = 6f;
    public float airSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;

    public LayerMask groundLayer;

    Vector3 velocity;

    public bool isGrounded;
    public bool isSprinting { private set; get; }

    public float x, z;

    void Start()
    {
        Debug.Log(GameManager.thisPlayer);
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        isSprinting = Input.GetKey(KeyCode.LeftShift);
    }

    void FixedUpdate()
    {
        velocity.y += gravity * Time.deltaTime;
		
		var inputVector = new Vector3(x, 0, z);

        Vector3 move = transform.right * x + transform.forward * z;

        if (isGrounded && !isSprinting)
        {
			var inputMag = (inputVector.magnitude > 1) ? move = move.normalized * speed : move = move * speed;
            //move = move * speed;
        }
        else if (isGrounded && isSprinting)
        {
			var inputMag = (inputVector.magnitude > 1) ? move = move.normalized * sprintSpeed : move = move * sprintSpeed;
            //move = move * sprintSpeed;
        }
        else
        {
			var inputMag = (inputVector.magnitude > 1) ? move = move.normalized * airSpeed : move = move * airSpeed;
            //move = move * airSpeed;
        }

        move.y = velocity.y;
        controller.Move(move * Time.deltaTime);
    }
}
