using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public float gravity;
   public Vector2 velocity;
   public float jumpVelocity = 20;
   public float groundHeight = 10;
   public bool isGrounded = false;
   public bool isHoldingJump  = false;
    void Start()
    {
        
    }
    void Update()
    {
        if(isGrounded) 
        {
            if (Input.touchCount > 0){
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
            }
        }
        if (Input.touchCount < 0)
        {
            Touch touch = Input.GetTouch(1);
            isHoldingJump = false;
        }
    }

    private void FixedUpdate() {
       Vector2 pos = transform.position;
       if(!isGrounded) {
        pos.y += velocity.y * Time.fixedDeltaTime;
        if(!isHoldingJump) 
        {
             velocity.y += gravity * Time.fixedDeltaTime;
        }
       
        if(pos.y <= groundHeight) {
            pos.y = groundHeight;
            isGrounded = true;
        }
       }
       transform.position = pos;
    }

}
