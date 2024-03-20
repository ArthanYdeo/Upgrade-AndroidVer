using System.Collections;
using TMPro;
using UnityEngine;
using LMNT;

public class NewPrologueTTS : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [TextArea(3, 15)]
    public string[] lines;
    public float textSpeed;
    public float scrollSpeed; // Speed of auto-scrolling

    private int index;
    private bool lineDisplayed = false;
    private LMNTSpeech lmntSpeech;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        gameObject.SetActive(true);

        // Get reference to LMNTSpeech component
        lmntSpeech = GetComponent<LMNTSpeech>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (lineDisplayed)
            {
                StartCoroutine(SpeakLine());
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                lineDisplayed = true;
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
            ScrollText();
        }
    }

    void ScrollText()
    {
        // Increment the vertical scroll position based on scrollSpeed
        textComponent.transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
    }

    void nextLine()
    {
        index++;
        if (index < lines.Length)
        {
            textComponent.text = string.Empty;
            lineDisplayed = false;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    IEnumerator SpeakLine()
    {
        StopAllCoroutines();
        textComponent.text = lines[index];

        // Speak the line using LMNTSpeech
        lmntSpeech.dialogue = lines[index];
        yield return StartCoroutine(lmntSpeech.Talk());

        // Move to the next line
        nextLine();
    }
}
