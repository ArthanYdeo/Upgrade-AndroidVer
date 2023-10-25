using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using PlayerControls;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 400;
    private PlayerControl controls;
    float direction = 0;
    public Rigidbody2D playerRB;
    public GameManagerScript gameManager;
    public Animator animator;
    bool isGrounded;

    private void Awake() {
        controls = new PlayerControls();
        controls.Enable();

        controls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Land.Jump.performed += ctx => Jump();
    }

    private void Start()
    {
        gameManager.DialogueIntro();
        Time.timeScale = 0f;
    }

    private void Update()
    {
       
    }
    private void FixedUpdate() 
    {
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);
    }


    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
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
