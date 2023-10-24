using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnScreenButton : MonoBehaviour
{
    public string horizontalAxis = "Horizontal"; // The horizontal axis to control
    public float movementSpeed = 5f;             // The movement speed
    public string jumpButton = "Jump";           // The jump button
    public float jumpForce = 10f;                // The force applied when jumping
    public Animator animator;                    // Reference to the Animator component
    public Rigidbody2D rb;                       // Reference to the Rigidbody2D component

    private bool isJumping = false;
    private bool isFacingRight = true;

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        UpdateAnimations();
    }

    private void HandleInput()
    {
        // Handle left and right movement
        float horizontalInput = Input.GetAxis(horizontalAxis);
        MoveCharacter(horizontalInput);

        // Handle jump input
        if (Input.GetButtonDown(jumpButton) && !isJumping)
        {
            Jump();
        }
    }

    private void MoveCharacter(float horizontalInput)
    {
        // Move the character
        Vector2 movement = new Vector2(horizontalInput, 0f) * movementSpeed * Time.deltaTime;
        rb.velocity = new Vector2(movement.x, rb.velocity.y);

        // Flip the character if moving in the opposite direction
        if (horizontalInput > 0 && !isFacingRight || horizontalInput < 0 && isFacingRight)
        {
            FlipCharacter();
        }
    }

    private void Jump()
    {
        // Jumping logic
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
    }

    private void UpdateAnimations()
    {
        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", isJumping);
    }

    private void FlipCharacter()
    {
        // Flip the character's scale to change direction
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump state when the character lands
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}