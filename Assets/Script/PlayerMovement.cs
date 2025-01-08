using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float movementSpeed = 10f;
    public float jumpForce = 30f;
    public Vector2 moveDirection = new Vector2(0, 1);
    public bool canJump;

    public LayerMask groundLayer; 
    public float groundCheckDistance = 1f;
    public RaycastHit terrainHit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // Continuous movement based on input
        rb.velocity = new Vector2(moveDirection.x * movementSpeed, rb.velocity.y);

        if (Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if(canJump)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Started)
        {
            // Update moveDirection when input is performed
            moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            // Stop movement when input is canceled
            moveDirection = Vector2.zero;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the raycast in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
