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
            Destroy(collision.gameObject);
            collectibles++;
            scrollText.text = "Score: " + collectibles;
        }
        if (collectibles == 4)
        {
            gameManager.Dialogue();
            gameManager.gameWin();
            Time.timeScale = 0;
            
        }
    }
}
