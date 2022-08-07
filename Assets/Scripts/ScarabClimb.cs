using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets;

public class ScarabClimb : MonoBehaviour
{
    public CharacterController controller;
    bool inside = false;
    public float speedUpDown = 3.2f;
    public ThirdPersonMovement TPSInput;

    // Start is called before the first frame update
    void Start()
    {
        TPSInput = GetComponent<ThirdPersonMovement>();
        inside = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inside == true && Input.GetAxisRaw("Vertical") > 0)
        {
            controller.transform.position += Vector3.up / speedUpDown;
        }
        if (inside == true && Input.GetAxisRaw("Vertical") < 0)
        {
            controller.transform.position += Vector3.down / speedUpDown;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Climbing Wall")
        {
            TPSInput.enabled = false;
            inside = !inside;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Climbing Wall")
        {
            TPSInput.enabled = true;
            inside = !inside;
        }
    }
}
