using LMNT;
using UnityEngine;
using UnityEngine.UI;

namespace LMNT
{
    public class DialogueTriggerScript : MonoBehaviour
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
    }
}
