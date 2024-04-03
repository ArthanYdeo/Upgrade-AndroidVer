using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    public GameManagerScript gameManager;
    private int collectibles = 0;
    [SerializeField] private TextMeshProUGUI scrollText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectibles"))
        {
            // Check if the collectible has already been collected
            if (!collision.gameObject.GetComponent<Collectible>().IsCollected)
            {
                // Mark the collectible as collected
                collision.gameObject.GetComponent<Collectible>().IsCollected = true;

                Destroy(collision.gameObject);
                collectibles++;
                scrollText.text = "Score: " + collectibles;

                if (collectibles == 5)
                {
                    UnlockNewLevel();
                    gameManager.Dialogue();
                    gameManager.gameWin();
                    Time.timeScale = 0;
                }
            }
        }
    }
    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
