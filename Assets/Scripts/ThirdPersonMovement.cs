using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using console
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -9.81f/2f;
    public float jumpHeight = 3f;
    //public float elevationSpeed = 3f;
    public float glideSpeed = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool gliding = false;
    //bool flying = false;

    private float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //check if the bird is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        /*
        check for jump while jumping and not flying and start flying
        if (!isGrounded && Input.GetButtonDown("Jump") && !flying)
        {
            StartFlying();
        }
        
        //check if flying and descend
        if (flying && Input.GetButton("Descend"))
        {
            //move controller down
            controller.Move(Vector3.down * elevationSpeed * Time.deltaTime);
        }

        //check if flying and ascend
        if (flying && Input.GetButton("Jump"))
        {
            //move controller up
            controller.Move(Vector3.up * elevationSpeed * Time.deltaTime);
        }

        if (flying && isGrounded) 
        {
            StopFlying();
        }
        */
 
        //Set velocity to -2 if grounded and not gliding
        if (isGrounded && velocity.y < 0)
        {
            gliding = false;
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && !isGrounded && !gliding)
        {
            gliding = true;
            velocity.y =  -glideSpeed;
        }

        //Apply gravity and velocity of gravity to the Player
        if (!gliding)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        //Let the Player jump if not grounded or flying
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    //Enable flying mode
    // void StartFlying() {
    //     flying = true;
    //     velocity.y = 0;
    //     controller.Move(velocity * Time.deltaTime);
    //     //Start flying animation
    //     gravity = 0;
    //     //start bobbing
    //     Debug.Log("Start Flying");
    // }

    // void StopFlying() {
    //     flying = false;
    //     //Stop flying animation
    //     gravity = -19.62f;
    //     //stop bobbing
    //     Debug.Log("Stop Flying");
    // }
}
