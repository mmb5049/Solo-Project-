using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public enum State
    {
        FaceLeft,
        FaceRight
    }

    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float movementSpeed = 10f;
    public float maxJumpForce = 30f;
    public float minJumpForce = 10f; // Force for a small hop
    public Vector2 moveDirection = new Vector2(0, 1);
    public bool canJump;
    public Animator animationControl;

    public LayerMask groundLayer;
    public float groundCheckDistance = 2f;
    public RaycastHit terrainHit;

    private bool isJumping = false;
    public float jumpPressDuration = 0f;
    public float maxJumpDuration = 3f;


    public State states;


    void Start()
    {
        states = State.FaceRight;

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


        // Increment jumpPressDuration if the player is holding the jump button
        if (isJumping)
        {
            jumpPressDuration += Time.deltaTime;

            if (jumpPressDuration > maxJumpDuration)
            {
                jumpPressDuration = maxJumpDuration;
                isJumping = false; // End jumping when the max duration is reached
            }

            // Apply dynamic jump force
            float jumpForce = Mathf.Lerp(minJumpForce, maxJumpForce, jumpPressDuration / maxJumpDuration);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (moveDirection.x > 0 && states != State.FaceRight)
        {
            Flip();
            states = State.FaceRight;
        }
        else if (moveDirection.x < 0 && states != State.FaceLeft)
        {
            Flip();
            states = State.FaceLeft;
        }

        animationControl.SetBool("Moving", moveDirection.x != 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && canJump)
        {
            isJumping = true;
            jumpPressDuration = 0f; // Reset duration
            rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity for consistent jumps
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            isJumping = false; // Stop jumping when the button is released
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

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
}