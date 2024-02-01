using LMNT;
using UnityEngine;
using UnityEngine.UI;

namespace LMNT
{
    /*public class DialogueTriggerScript : MonoBehaviour
    {
        private LMNTSpeech speech;

        void Start()
        {
            speech = GetComponent<LMNTSpeech>();
            StartCoroutine(speech.Prefetch());
        }

        // This method is called when the button is clicked
        public void OnButtonClick()
        {
            StartCoroutine(speech.Talk());
        }
    }*/
    public class DialogueTriggerScript : MonoBehaviour
    {
        private LMNTSpeech speech;
        private Coroutine speechCoroutine;  // Store the coroutine reference

        void Start()
        {
            speech = GetComponent<LMNTSpeech>();
            StartCoroutine(speech.Prefetch());
        }

        // This method is called when the button is clicked
        public void OnButtonClick()
        {
            // Check if the coroutine is running
            if (speechCoroutine != null)
            {
                // If running, stop the coroutine and set the reference to null
                StopCoroutine(speechCoroutine);
                speechCoroutine = null;
            }
            else
            {
                // If not running, start the coroutine and store the reference
                speechCoroutine = StartCoroutine(speech.Talk());
            }
        }
    }
}
