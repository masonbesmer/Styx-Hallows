using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController CharController;
    public static float PlayerSpeed = 10f;
    public float gravity = -9.81f;
    public float JumpHeight = 3f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public static AudioSource audioSrc;
    Animator animate;

    // Start is called before the first frame update
    void Start()
    {
        //Readying Cosmetic Components And Resetting Player Speed to 20
        audioSrc = GetComponent<AudioSource>();
        animate = GetComponent<Animator>();
        PlayerSpeed = 20;
    }

    // Update is called once per frame
    void Update()
    {
        //Animate Weapon Bobbing
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            animate.SetTrigger("IsRun");
        }
        else if (!Input.GetKey("w") || !Input.GetKey("a") || !Input.GetKey("s") || !Input.GetKey("d"))
        {
            animate.SetTrigger("NotRun");
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }

        //Check if the player is grounded

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        //Player Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        CharController.Move(move * PlayerSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        CharController.Move(velocity * Time.deltaTime);
    }
}
