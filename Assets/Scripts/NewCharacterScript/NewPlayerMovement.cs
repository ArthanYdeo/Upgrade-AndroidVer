using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    PlayerControls controls;

    private bool isAttacking = false;

    float direction = 0;
    public float speed = 400;
    bool isFacingRight = true;

    public float jumpForce = 5;
    bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public Rigidbody2D playerRB;
    public Animator animator;

    public GameManagerScript gameManager;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Land.Jump.performed += ctx => Jump();

        // Start attack when pressed
        controls.Land.Attack.started += ctx => StartAttack();
        // Stop attack when released
        controls.Land.Attack.canceled += ctx => StopAttack();
    }
    private void Start()
    {
        gameManager.DialogueIntro();
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y); 
        animator.SetFloat("Speed", Mathf.Abs(direction));

        if (isFacingRight && direction < 0 || !isFacingRight && direction > 0 )
            Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()
    {
        if(isGrounded)
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
    }

    void StartAttack()
    {
        if (!isAttacking)
        {
            
            animator.SetTrigger("Attack");
            isAttacking = true; 
        }
    }

    void StopAttack()
    {
        // Check if the Animator component is not null before accessing it
        if (animator != null)
        {
            // Set trigger only if animator is not null
            animator.ResetTrigger("Attack");
        }

        isAttacking = false;
    }


    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            // Player collided with an obstacle, game over
            gameManager.Dialogue();
            gameManager.gameWin(); 
            Time.timeScale = 0; // Pause the game
        }
        
    }*/
}
