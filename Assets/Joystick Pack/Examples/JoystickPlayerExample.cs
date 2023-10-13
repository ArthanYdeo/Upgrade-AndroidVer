using UnityEngine;
using UnityEngine.EventSystems;
public class JoystickPlayerExample : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public VariableJoystick variableJoystick;
    public Rigidbody2D rb;
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Jump input
        if (isGrounded && (Input.GetButtonDown("Jump") || variableJoystick.Vertical > 0.5))
        {
            Jump();
        }
    }

    public void FixedUpdate()
    {
        // Movement input
        float horizontalInput = Input.GetAxis("Horizontal") + variableJoystick.Horizontal;

        // Make sure to clamp the value to prevent overly fast movement
        horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);

        // Set the velocity based on the horizontal input
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
