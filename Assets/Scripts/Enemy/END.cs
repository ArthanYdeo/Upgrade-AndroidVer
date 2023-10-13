using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class END : MonoBehaviour
{
    public GameObject ending;
    public GameManagerScript gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Ending();
            Time.timeScale = 0f;
        }

    }

    public void Ending()
    {
        ending.SetActive(true);
        gameManager.gameWin();
    }

}
