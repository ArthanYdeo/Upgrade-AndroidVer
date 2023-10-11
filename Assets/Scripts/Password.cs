using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    private InputField inputField;

    private void Start()
    {
        // Get the Input Field component.
        inputField = GetComponent<InputField>();

        // Add a listener to the input field's value changed event.
        inputField.onValueChanged.AddListener(MaskPassword);
    }

    private void MaskPassword(string newValue)
    {
        // Replace the displayed text with asterisks.
        string maskedPassword = new string('*', newValue.Length);
        inputField.text = maskedPassword;
    }

    public string GetPassword()
    {
        // Retrieve the entered password.
        return inputField.text;
    }
}