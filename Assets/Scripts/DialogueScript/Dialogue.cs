using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]

/*public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [TextArea(3, 15)]
    public string[] lines;
    public float textSpeed;

    // Add a reference to LMNTSpeech
    public LMNT.LMNTSpeech lmntSpeech;

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        gameObject.SetActive(true);
    }

    bool lineDisplayed = false;

    void Update()
    {
        Debug.Log("Update method running!");

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse click detected!");

            if (lineDisplayed)
            {
                Debug.Log("Line is fully displayed. Calling SpeakLine.");
                StartCoroutine(SpeakLine());
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                lineDisplayed = true;
                Debug.Log("Line is not fully displayed. Setting lineDisplayed to true.");
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
        }
    }

    void nextLine()
    {
        // Increment the index to move to the next line
        index++;

        if (index < lines.Length)
        {
            // If there are more lines, reset the text, reset lineDisplayed, and type the next line
            textComponent.text = string.Empty;
            lineDisplayed = false;
            StartCoroutine(TypeLine());
        }
        else
        {
            // If all lines are displayed, deactivate the dialogue
            Debug.Log("Dialogues completed!");
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    IEnumerator SpeakLine()
    {
        Debug.Log("SpeakLine method running!");

        // Stop the typewriter effect
        StopAllCoroutines();
        textComponent.text = lines[index];

        // Speak the line using LMNTSpeech
        lmntSpeech.dialogue = lines[index];
        yield return StartCoroutine(lmntSpeech.Talk());

        // Move to the next line
        nextLine();
    }


}*/
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [TextArea(3,15)]

    public GameObject objectivePopup;

    public string[] lines;
    public float textSpeed;

    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        gameObject.SetActive(true);

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
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
        }
    }
    void nextLine()
    {
        if (index < lines.Length - 1) 
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            
        }
        else
        {
            // Dialogue ended, show objective popup
            if (objectivePopup != null)
            {
                objectivePopup.SetActive(true);
            }

            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

