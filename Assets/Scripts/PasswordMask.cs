using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PasswordMask : MonoBehaviour
{
     public TMP_InputField passwordInput;

    // Call this method when the value of the password input field changes
    public void MaskPasswordInput(string text)
    {
        passwordInput.text = new string('*', text.Length);
    }
}
