using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    public RectTransform content; // Reference to the content of the scrollable panel
    public float scrollSpeed = 20f; // Speed of scrolling in units per second

    private bool scrolling = false;

    void Start()
    {
        // Set the initial scrolling state to true if the content is taller than the panel
        if (content.rect.height > GetComponent<RectTransform>().rect.height)
        {
            scrolling = true;
        }
    }

    void Update()
    {
        if (scrolling)
        {
            // Calculate the amount to scroll based on the scroll speed and deltaTime
            float scrollAmount = scrollSpeed * Time.deltaTime;

            // Move the content upwards
            content.anchoredPosition += Vector2.up * scrollAmount;

            // If the top of the content reaches the top of the panel, stop scrolling
            if (content.anchoredPosition.y >= 0)
            {
                scrolling = false;
            }
        }
    }
}
