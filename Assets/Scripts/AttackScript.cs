using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    Animator myAnimation;

    private void Start()
    {
        myAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            myAnimation.SetBool("Attack", true);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            myAnimation.SetBool("Attack", false);
        }
    }
}
