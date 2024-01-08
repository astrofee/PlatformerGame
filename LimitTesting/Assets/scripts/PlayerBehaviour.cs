using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 5f;
    [SerializeField] float airSpeed = 4f; 
    [SerializeField] float minDistance = 1f; // Minimum distance to start moving towards the target
    public GameObject speedRingPrefab; // The prefab to instantiate when dashing
    [SerializeField] VFXManager vfxManager;
    private TargetBehaviour trgt;
    public float jumpForce = 5f; // The force of the jump
    [SerializeField] float dashForce = 10f; // The force of the dash
    [SerializeField] float dashDuration = 0.2f; // The duration of the dash in seconds
    private bool isDashing = false; // Whether the player is currently dashing
    private bool isGrounded = false;
    private Rigidbody rb; 

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        trgt = target.GetComponent<TargetBehaviour>();
    }
    public void ToggleGrounded()
    {
        if(isGrounded)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
            if(!trgt.canmove)
            {
                rb.velocity = Vector3.zero;
                trgt.canmove = true;
            }
        }
    }
    void Dash()
    {
        // Start dashing
        isDashing = true;
        Vector3 dashDirection = rb.velocity.normalized;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        vfxManager.CreateVFX(speedRingPrefab, transform.position, transform.rotation, null);
        // Stop dashing after a certain period of time
        Invoke("StopDashing", dashDuration);
        // Instantiate the speedring prefab at the player's position
    }
    public void StopDashing()
    {
        Debug.Log("Stop dashing");
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        isDashing = false;
        if(!isGrounded)
        {
            trgt.canmove = false;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
            Vector3 targetPosition = transform.position + transform.forward * trgt.maxDistance;
            target.position = new Vector3(targetPosition.x, targetPosition.y + trgt.groundOffset, targetPosition.z);
            
    }
    void Update()
    {
        if(isDashing || !trgt.canmove)
        {
            Vector3 targetPosition = transform.position + transform.forward * trgt.maxDistance;
            target.position = new Vector3(targetPosition.x, targetPosition.y + trgt.groundOffset, targetPosition.z);
            return;
        }

        if (!isGrounded)
        {
            trgt.groundOffset += Time.deltaTime; // Increment groundOffset based on the time that isGrounded is false
        }
        else
        {
             // Interpolate groundOffset back to 0 when isGrounded is true
            trgt.groundOffset = Mathf.Lerp(trgt.groundOffset, 0f, 8f * Time.deltaTime); // Reset groundOffset when isGrounded is true
        }

        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0; // Keep only the horizontal direction

        // Check if the target is far enough away
        if (directionToTarget.magnitude >= minDistance)
        {
            // Create the rotation we need to be in to look at the target
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // look at target point
            transform.rotation = targetRotation;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isDashing)
        {
            // Add an upward force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        if (!isDashing && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
            Dash();
        }
    }

    private void LateUpdate()
    {
        if(isDashing || !trgt.canmove)
        {
            return;
        }
        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.y = 0; // Keep only the horizontal direction

        // Check if the target is far enough away
        if (directionToTarget.magnitude >= minDistance)
        {
        // Calculate the distance the object will move this frame
            float distanceToMove = (isGrounded ? speed : airSpeed) * Time.deltaTime;

            // If the object is about to overshoot the target, stop it at the target
            if (directionToTarget.magnitude <= distanceToMove)
            {
                transform.position = target.position;
            }
            else
            {
                // Move towards the target
                transform.position += directionToTarget.normalized * distanceToMove;
            }
        
        }
    }
}
