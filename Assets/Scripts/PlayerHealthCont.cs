using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthCont : MonoBehaviour
{
    public int maxHealth = 500;
    public int currentHealth;

    public HealthBar healthBar;
    public GameManagerScript gameManager;




    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
            //TakeDamage(20);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(2);
            
        }
        if (collision.gameObject.CompareTag("Outside"))
        {
            TakeDamage(maxHealth);

        }
        if (currentHealth <= 0)
        {
            gameManager.gameOver();
            Time.timeScale = 0;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(5);
        }
        if (currentHealth <= 0)
        {
            gameManager.gameOver();
            Time.timeScale = 0;
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
