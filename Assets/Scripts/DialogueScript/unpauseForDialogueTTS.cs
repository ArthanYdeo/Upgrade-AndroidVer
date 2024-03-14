using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unpauseForDialogueTTS : MonoBehaviour
{
    // Reference to the button that will unpause the game
    public Button unpauseButton;

    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the button's onClick event to call the Unpause method
        unpauseButton.onClick.AddListener(Unpause);
    }

    void Unpause()
    {
        Time.timeScale = 1f; // Set time scale to 1 to unpause the game
    }
}
