using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_08_BlendTreeanimation : MonoBehaviour
{
    // v8 1D Blend Anim tree
    public float maximumSpeed; // rename public var speed
        
    public float rotationSpeed;
   
    // v4 jump
    public float jumpSpeed;
    private float ySpeed;
    private float originalStepOffset;

    // v5 - improve jump
    public float jumpButtonGracePeriod;
    private float? lastGroundedTime; // nullable float type
    private float? jumpButtonPressedTime;

    // v6. animation
    private Animator animator;

    private CharacterController characterController;

    // v5. jump - Add this boolean to track jumping state
    private bool IsJumping;
    
    void Start() {
        characterController = GetComponent<CharacterController>();
        // v4 jump
        originalStepOffset = characterController.stepOffset;
        // v6. animation
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Old input system
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = transform.TransformDirection(new Vector3(horizontalInput, 0, verticalInput));

        // v8 1D Blend Anim tree
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude *= 0.5f; // Reduce magnitude to reflect slower movement
            animator.SetBool("IsWalking", true); // Trigger walking animation
        }
        else
        {
            animator.SetBool("IsWalking", false); // Default to running animation
        }

        // Set Float of animator component to blend animations.
        animator.SetFloat("Input Magnitude", inputMagnitude, 0.15f, Time.deltaTime);

        float speed = inputMagnitude * maximumSpeed;

        // Normalize direction vector so that it has a range of 0-1
        movementDirection.Normalize();

        // v4 - Jump. Update ySpeed with gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // v5 - Improve jump
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time; // Record the time when grounded

            if (Input.GetButtonDown("Jump"))
            {
                jumpButtonPressedTime = Time.time; // Record the time when the jump button was pressed
            }

            if (IsJumping)
            {
                IsJumping = false;
                animator.SetBool("IsJumping", false); // Exit jump animation when grounded
                animator.SetBool("IsWalking", true); // Resume walking animation after landing
            }
        }
        else
        {
            if (IsJumping)
            {
                animator.SetBool("IsJumping", true);  // Keep jump animation active in the air
                animator.SetBool("IsWalking", false); // Stop walking animation while jumping
            }
        }

        // v5. improve jump replace if (characterController.isGrounded)
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset; // reset characterController stepOffset
            ySpeed = -0.5f;  // reset ySpeed 

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;   // apply jumpSpeed to ySpeed
                IsJumping = true;
                animator.SetBool("IsJumping", true);  // Trigger jump animation
                jumpButtonPressedTime = null;  // Reset jump button pressed time to avoid multiple jumps in grace period
                lastGroundedTime = null;  // Reset last grounded time to avoid multiple jumps in grace period
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        // v4 - Jump. Local var vector3 velocity
        // Add ySpeed to velocity
        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;

        // Time.deltaTime is required for the characterController Move method
        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            // Change character to point in the direction of movement
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
