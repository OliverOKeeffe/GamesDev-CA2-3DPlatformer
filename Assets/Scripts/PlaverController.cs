using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //v8 1D Blend Anim tree
    public float maximumSpeed; //rename public var speed
        
    public float rotationSpeed;
   
    //v4 jump
    public float jumpSpeed;
    private float ySpeed;
    private float originalStepOffset;

    //v5 - improve jump
    public float jumpButtonGracePeriod;
    private float? lastGroundedTime; //nullable floattype
    private float? jumpButtonPressedTime;

    //v6. animation
    private Animator animator;

    private CharacterController characterController;
    
    
    void Start() {
        characterController = GetComponent<CharacterController>();
        //v4 jump
        originalStepOffset = characterController.stepOffset;
        //v6. animation
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the character's facing direction
        Vector3 forward = transform.forward * verticalInput;  // Forward (W/S)
        Vector3 right = transform.right * horizontalInput;    // Right (A/D)
        Vector3 movementDirection = forward + right;

        // v8 1D Blend Anim tree
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        // Adjust movement speed when shift key is held
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2; // Half speed when shift is pressed
        }

        // Set animation parameters for blending
        animator.SetFloat("Input Magnitude", inputMagnitude, 0.15f, Time.deltaTime);

        // Calculate the actual movement speed
        float speed = inputMagnitude * maximumSpeed;

        // Normalize the movement direction
        movementDirection.Normalize();

        // v4 - Jump. Update ySpeed with gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // v5. improve jump - handle jump grace period
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time; // Assign time when grounded
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time; // Assign time when jump button is pressed
        }

        // Jump logic within grace period
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset; // Reset step offset
            ySpeed = -0.5f; // Small negative speed to stay grounded
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed; // Apply jump speed
                jumpButtonPressedTime = null; // Reset jump button press time
                lastGroundedTime = null; // Reset grounded time
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        // Combine movement direction with gravity (ySpeed)
        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;

        // Apply the movement to the character
        characterController.Move(velocity * Time.deltaTime);

        // Rotate the character to face the direction of movement
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
