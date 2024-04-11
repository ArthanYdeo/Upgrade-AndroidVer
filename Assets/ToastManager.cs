using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToastManager : MonoBehaviour
{
    public Text toastText;
    public GameObject toastPanel;

    private Coroutine currentToastCoroutine;

    public void ShowToast(string message, float duration)
    {
        if (currentToastCoroutine != null)
        {
            StopCoroutine(currentToastCoroutine);
            toastPanel.SetActive(false);
        }

        toastText.text = message;
        toastPanel.SetActive(true);
        currentToastCoroutine = StartCoroutine(DismissToast(duration));
    }

    IEnumerator DismissToast(float duration)
    {
        yield return new WaitForSeconds(duration);
        toastPanel.SetActive(false);
    }
}
