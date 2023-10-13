using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

           if (Input.GetButtonDown("Jump"))
            {
                jump = false;
                animator.SetBool("IsJumping", false);
            }

           
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
    public void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
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

