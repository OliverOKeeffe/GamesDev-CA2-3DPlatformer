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

        Vector3 movementDirection = transform.TransformDirection(new Vector3(horizontalInput, 0, verticalInput));
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

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
        float speed = inputMagnitude * maximumSpeed;
        movementDirection.Normalize();
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (!characterController.isGrounded && ySpeed < 0)
        {
            animator.SetBool("isFalling", true);
        }
    }
}