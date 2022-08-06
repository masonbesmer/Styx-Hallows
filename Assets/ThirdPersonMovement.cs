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
    public float gravity = -19.62f;
    public float jumpHeight = 3f;
    public float elevationSpeed = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float bobbingSpeed = 30f;
    public float bobAmount = 100f;

    Vector3 velocity;
    bool isGrounded;
    bool flying = false;
    bool bobbing = false;
    bool bobBlock = false;

    private float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        bobBlock = false;
        //check if the bird is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //check for jump while jumping and not flying and start flying
        if (!isGrounded && Input.GetButtonDown("Jump") && !flying)
        {
            StartFlying();
        }

        //check if flying and descend
        if (flying && Input.GetButton("Descend"))
        {
            StopBobbing();
            //move controller down
            controller.Move(Vector3.down * elevationSpeed * Time.deltaTime);
            bobBlock=true;
        }

        //check if flying and ascend
        if (flying && Input.GetButton("Jump"))
        {
            StopBobbing();
            //move controller up
            controller.Move(Vector3.up * elevationSpeed * Time.deltaTime);
            bobBlock=true;
        }

        if (flying && Input.GetButtonUp("Jump")) {
            StartBobbing();
        }

        if (flying && Input.GetButtonUp("Descend"))
        {
            StartBobbing();
        }

        if (flying && isGrounded) {
            StopFlying();
        }

        if (!flying && bobbing) {
            StopBobbing();
        }
 
        //set birds velocity to -2 if grounded and not flying
        if (isGrounded && velocity.y < 0 && !flying)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //apply gravity and velocity of gravity to the bird
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //let the bird jump if not grounded or flying
        if(Input.GetButtonDown("Jump") && isGrounded && !flying)
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
    void StartFlying() {
        flying = true;
        velocity.y = 0;
        controller.Move(velocity * Time.deltaTime);
        //Start flying animation
        gravity = 0;
        //start bobbing
        StartBobbing();
        Debug.Log("Start Flying");
    }

    void StopFlying() {
        flying = false;
        //Stop flying animation
        gravity = -19.62f;
        //stop bobbing
        StopBobbing();
        Debug.Log("Stop Flying");
    }

    void StartBobbing() {
        if (!bobbing && !bobBlock) {
            StartCoroutine(Bob());
            Debug.Log("Start Bobbing");
            bobbing = true;
        } else {
            Debug.Log("Bobbing already started or bobbing blocked");
        }
    }

    void StopBobbing() {
        if (bobbing) {
            StopCoroutine(Bob());
            bobbing = false;
            Debug.Log("Stop Bobbing");
        } else {
            Debug.Log("Bobbing is not running");
        }
    }
    
    IEnumerator Bob() {
        while (true) {
            Debug.Log("Bobbing");
            for (int i=0; i<bobAmount; i++) {
                //move controller down
                //controller.Move(Vector3.up * bobbingSpeed * Time.deltaTime);
                controller.Move(Vector3.up * 2 * Time.deltaTime);
            }
            yield return new WaitForSeconds(0.1f);
            for (int i=0; i<bobAmount; i++) {
                //move controller down
                //controller.Move(Vector3.down * bobbingSpeed * Time.deltaTime);
                controller.Move(Vector3.down * 2 * Time.deltaTime);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
