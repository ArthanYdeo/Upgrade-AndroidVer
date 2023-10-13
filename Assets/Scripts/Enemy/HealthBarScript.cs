using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TakeDamage(20);

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
    /*public float Hitpoints;
      public float MaxHitpoints = 5;
      public EnemyHealth Healthbar;
      private float damage;

      // Start is called before the first frame update
      void Start()
      {
          Hitpoints = MaxHitpoints;
          Healthbar.SetHealth(Hitpoints, MaxHitpoints);
      }

   public void TakeHit (float damage)
      {

          Hitpoints -= damage;
          Healthbar.SetHealth(Hitpoints, MaxHitpoints);

          if (Hitpoints <= 0)
          {
              Destroy(gameObject);
          }
      }

      private void OnTriggerEnter2D(Collider2D collision)
      {

          if (collision.gameObject.CompareTag("Player"))
              {
              Hitpoints -= damage;
              Healthbar.SetHealth(Hitpoints, MaxHitpoints);

              if (Hitpoints <= 0)
              {
                  Destroy(gameObject);
              }
          }
      }*/

}
