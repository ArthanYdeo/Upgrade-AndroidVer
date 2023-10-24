using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;

    public GameManagerScript gameManager;

    private void Start()
    {
        gameManager.DialogueIntro();
        Time.timeScale = 0f;
    }

    public void Update()
    {
        // Use buttons for movement
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void FixedUpdate()
    {
        // Use Touch for mobile controls
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).position.x < Screen.width / 2)
            {
                horizontalMove = -runSpeed;
            }
            else if (Input.GetTouch(i).position.x > Screen.width / 2)
            {
                horizontalMove = runSpeed;
            }
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    // Modify this method based on your button setup in the UI
    public void OnButtonJumpClick()
    {
        jump = true;
        animator.SetBool("IsJumping", true);
    }
}
