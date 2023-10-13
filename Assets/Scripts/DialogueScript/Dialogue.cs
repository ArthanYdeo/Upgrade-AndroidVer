using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [TextArea(3,15)]

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
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
