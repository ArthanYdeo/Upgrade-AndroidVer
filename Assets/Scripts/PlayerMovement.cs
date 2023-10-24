using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    public GameManagerScript gameManager;

    private void Start()
    {
        gameManager.DialogueIntro();
        Time.timeScale = 0f;
    }

    public void Update()
    {
        // Handle touch or button input
        horizontalMove = GetHorizontalInput() * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // Handle jump using UI button
        if (IsJumpButtonPressed())
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    public float GetHorizontalInput()
    {
        float horizontalInput = 0f;

        // Check if UI buttons are pressed
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }

        return horizontalInput;
    }

    public bool IsJumpButtonPressed()
    {
        // Check if UI jump button is pressed or space key is pressed
        return Input.GetButtonDown("Jump");
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            // Player collided with an obstacle, game over
            gameManager.Dialogue();
            gameManager.gameWin();
            Time.timeScale = 0; // Pause the game
        }
    }
}
