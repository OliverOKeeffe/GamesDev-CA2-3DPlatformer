using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_08_BlendTreeanimation : MonoBehaviour
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
        //old input system
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        //v8 1D Blend Anim tree
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
       

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            inputMagnitude /= 2; //1/2 speed
        }

        //set Float of animator component to blend animations.
        animator.SetFloat("Input Magnitude", inputMagnitude, 0.15f, Time.deltaTime);
        
        float speed = inputMagnitude * maximumSpeed;


        //Normalize diretion vector so that it has a range of 0-1
        movementDirection.Normalize();

        //v4 - Jump. update ySpeed with Gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;
        //Debug.Log(ySpeed);

        //v5. improve jump
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time; // assign time since game started
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time; // assign time since game started
        }


        //v5. improve jump replace  if (characterController.isGrounded)
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {

            characterController.stepOffset = originalStepOffset;//reset characterController stepOffset
            ySpeed = -0.5f;  //reset ySpeed 
            //v5. improve jump. replace  Input.GetButtonDown("Jump")
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;   //apply jumpSpeed to ySpeed
                //v5. improve jump. reset nullables back to null
                // in order to avoid multiple jumps inside gracePeriod
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        // v4 - Jump. Local var vector3 velocity
        // add ySpeed to velocity

        //v8 replace magnitude with speed

        Vector3 velocity = movementDirection * speed;
       
        velocity.y = ySpeed;
       //Time.deltaTime is  required for the charControll Move method
       characterController.Move(velocity * Time.deltaTime);

        

        if (movementDirection != Vector3.zero)
        {
            //changes character to point in direction of movement. 
            //v6. animation 
            //animator.SetBool("isMoving", true);

            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime); 
        } else
        {
            //v6. animation 
            //animator.SetBool("isMoving", false);
        }
    }
}

