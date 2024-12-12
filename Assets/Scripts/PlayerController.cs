using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_08_BlendTreeanimation : MonoBehaviour
{
    public float maximumSpeed;
    public float rotationSpeed;
    public float jumpSpeed;
    private float ySpeed;
    private float originalStepOffset;
    public float jumpButtonGracePeriod;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private Animator animator;
    private CharacterController characterController;

    public Transform cameraTransform; // Reference to the camera transform

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movement direction relative to camera
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        Vector3 movementDirection = cameraForward * verticalInput + cameraTransform.right * horizontalInput;

        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        // Adjust walking speed and animation
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude *= 1f;
            animator.SetBool("IsWalking", false);
        }
        else
        {
            inputMagnitude *= 0.5f;
            animator.SetBool("IsWalking", true);
        }

        animator.SetFloat("InputMagnitude", inputMagnitude, 0.15f, Time.deltaTime);

        // Apply gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // Jumping and falling logic
        if (characterController.isGrounded)
        {
            ySpeed = -0.5f;
            animator.SetBool("IsFalling", false); // Not falling when grounded

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
                animator.SetBool("IsJumping", true); // Start jumping
            }
            else
            {
                animator.SetBool("IsJumping", false); // Reset jump animation
            }
        }
        else
        {
            if (ySpeed > 0)
            {
                animator.SetBool("IsJumping", true); // Jumping upwards
                animator.SetBool("IsFalling", false); // Not falling while jumping up
            }
            else if (ySpeed < 0)
            {
                animator.SetBool("IsFalling", true); // Falling downwards
                animator.SetBool("IsJumping", false); // Stop jump animation when falling
            }
        }

        // Character movement
        Vector3 velocity = movementDirection * (inputMagnitude * maximumSpeed);
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        // Rotation towards movement direction
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
