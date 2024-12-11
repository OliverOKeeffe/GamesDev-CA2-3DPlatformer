using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_08_BlendTreeanimation : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    private Animator animator;  // Reference to the Animator
    [SerializeField] private Transform camera;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintSpeed = 12f;
    [SerializeField] private float sprintTransitSpeed = 3f;
    [SerializeField] private float turningSpeed = 1f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 5f;

    private float verticalVelocity;  // Change 'walkSpeed' to 'speed'
    private float speed;  // Store the current speed
    private float moveInput;
    private float turnInput;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();  // Get the Animator component
    }

    private void Update()
    {
        InputManagement();
        Movement();
    }

    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    private void Movement()
    {
        GroundMovement();
        turn();

        // Update the animator parameters for Blend Tree using only InputMagnitude
        UpdateAnimationParameters();
    }

    private void GroundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move = transform.TransformDirection(move);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed * Time.deltaTime);
        }

        move.y = VerticalForceCalculation();  // Vertical movement (gravity and jump)

        move *= speed;  // Adjust movement speed

        controller.Move(move * Time.deltaTime);
    }

    private void turn()
    {
        // Use Mathf.Abs instead of Math.Abs
        if (Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0)
        {
            Vector3 currentLookDirection = camera.forward;
            currentLookDirection.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);
        }
    }

    private void UpdateAnimationParameters()
    {
        // Calculate input magnitude (this is the factor that drives the Blend Tree)
        float inputMagnitude = Mathf.Clamp01(new Vector3(turnInput, 0, moveInput).magnitude);

        // Update the "InputMagnitude" parameter in the animator for the Blend Tree
        animator.SetFloat("InputMagnitude", inputMagnitude);  // Ensure that the parameter name matches in the Animator

        // Handle jump movement (No additional animation parameters needed)
        if (!controller.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
    }

    private float VerticalForceCalculation()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;  // Slight negative velocity to keep the character grounded
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }
}
