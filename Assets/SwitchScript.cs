using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScript : MonoBehaviour
{
    public GameObject successPanel; // Reference to the success panel GameObject

    void Start()
    {
        successPanel.SetActive(false); // Make sure the success panel is initially inactive
    }

    // This method will be called when the button is clicked
    public void ButtonClicked()
    {
        successPanel.SetActive(true); // Activate the success panel when the button is clicked
    }
}
