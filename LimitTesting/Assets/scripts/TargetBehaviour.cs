using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    public Transform referenceTransform; // The transform we want to move relative to
    public Transform otherTransform; // The transform we want to follow the y value of
    public float speed = 5f; // The speed of movement
    public float maxDistance = 5f;
    public float groundOffset = 0f;
    private Vector3 startPosition; // The position where the object should start
    public bool canmove = true;

    void Start()
    {
        startPosition = transform.position; // Store the initial position
    }

    // Update is called once per frame
    Vector3 Move()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction = referenceTransform.TransformDirection(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            direction = referenceTransform.TransformDirection(Vector3.back);
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += referenceTransform.TransformDirection(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction += referenceTransform.TransformDirection(Vector3.right);
        }
        return direction;
    }
    void FixedUpdate()
    {
    Vector3 direction = Vector3.zero;
    if(canmove)
    {
        direction = Move();
    }

    // Only move on the x and z axes
    direction.y = 0;

    // If no input is detected, reset the position
    if (direction == Vector3.zero && canmove)
    {
        transform.position = startPosition;
    }
    else
    {
        // Move relative to the reference transform
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    // Follow the y value of another transform
    transform.position = new Vector3(transform.position.x, otherTransform.position.y + groundOffset, transform.position.z);
    startPosition = new Vector3(otherTransform.position.x, transform.position.y, otherTransform.position.z);

    Vector3 directionToOther = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(otherTransform.position.x, 0, otherTransform.position.z);
    if (directionToOther.magnitude > maxDistance)
    {
        directionToOther = directionToOther.normalized * maxDistance;
        transform.position = new Vector3(otherTransform.position.x, transform.position.y, otherTransform.position.z) + directionToOther;
    }
    }
}