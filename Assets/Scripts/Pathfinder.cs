using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] float rayCastOffset = 0.5f;
    [SerializeField] float detectionDistance = 2f;

    Transform target;
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float rotationalDamp = .5f;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Pathfinding();
        //Turn();
        Move();
    }

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        Debug.Log("Position: " + pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void Pathfinding()
    {
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;

        Debug.DrawRay(left, transform.forward * detectionDistance, Color.red);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.red);
        Debug.DrawRay(up, transform.forward * detectionDistance, Color.red);
        Debug.DrawRay(down, transform.forward * detectionDistance, Color.red);

        if (Physics.Raycast(left, transform.forward, out hit, detectionDistance))
        {
            raycastOffset += Vector3.up;
            Debug.Log("Hit: " + hit.transform.name);
        }
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance))
        {
            raycastOffset -= Vector3.up;
            Debug.Log("Hit: " + hit.transform.name);
        }

        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance))
        {
            raycastOffset += Vector3.right;
            Debug.Log("Hit: " + hit.transform.name);
        }
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
        {
            raycastOffset -= Vector3.right;
            Debug.Log("Hit: " + hit.transform.name);
        }

        Debug.Log("Offset: " + raycastOffset);

        if (raycastOffset != Vector3.zero)
            transform.Rotate(raycastOffset * 10f * Time.deltaTime);
        else
            Turn();
    }
}
