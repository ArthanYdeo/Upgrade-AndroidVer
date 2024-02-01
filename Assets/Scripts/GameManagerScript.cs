using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManagerScript : MonoBehaviour
{

    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public GameObject gamePauseUI;
    public GameObject dialogueBoxOutro;
    public GameObject dialogueBoxIntro;
    public GameObject dialogueBoxSynapse;
    public GameObject dialogueBoxCipher;
    public GameObject controlPanelUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ControlPanelUI()
    {
        controlPanelUI.SetActive(true);
    }
    public void DialogueBoxCipher()
    {
        dialogueBoxCipher.SetActive(true);
    }
    public void DialogueBoxSynapse()
    {
        dialogueBoxSynapse.SetActive(true);
    }
    public void DialogueIntro()
    {
        dialogueBoxIntro.SetActive(true);
    }
    public void Dialogue()
    {
        dialogueBoxOutro.SetActive(true);
    }
    public void gameWin()
    {
        gameWinUI.SetActive(true);
    }
    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void resume()
    {
        gamePauseUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void pause()
    {
        Time.timeScale = 0;
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Selection");
        Time.timeScale = 1;
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("Quit");
        Time.timeScale = 1;
    }

    public void selection()
    {
        SceneManager.LoadScene("Story");
        Time.timeScale = 1;
    }
}

