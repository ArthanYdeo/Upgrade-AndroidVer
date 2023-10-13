using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhit : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TakeDamage(10);

        }
        if (collision.gameObject.CompareTag("Outside"))
        {
            TakeDamage(maxHealth);

        }
        if (currentHealth == 0)
        {
           
            Destroy(gameObject);
        }



    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

    }
}
