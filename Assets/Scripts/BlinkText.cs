using UnityEngine;
using TMPro;
using System.Collections;

public class BlinkText : MonoBehaviour
{
    // Reference to the TextMeshPro component
    public TextMeshProUGUI textMeshPro;

    // Parameters for the breathing effect
    public float breathSpeed = 1.0f;
    public float breathAmount = 0.1f;

    // Initial scale of the text
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the TextMeshPro component is assigned
        if (textMeshPro != null)
        {
            // Store the initial scale of the text
            initialScale = textMeshPro.transform.localScale;

            // Start the breathing animation
            StartCoroutine(BreathingAnimation());
        }
        else
        {
            // Log a warning if the TextMeshPro component is not assigned
            Debug.LogWarning("TextMeshPro component is not assigned!");
        }
    }

    // Breathing animation coroutine
    IEnumerator BreathingAnimation()
    {
        while (true)
        {
            // Calculate the new scale based on a sine wave
            float scale = 1.0f + Mathf.Sin(Time.time * breathSpeed) * breathAmount;

            // Apply the new scale to the text
            textMeshPro.transform.localScale = initialScale * scale;

            // Wait for the next frame
            yield return null;
        }
    }
}
