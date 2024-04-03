using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    public Button[] buttons;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++) 
        {
            buttons[i].interactable = true;
        }
    }

    public void LoadLevel (int sceneIndex) 
    {
       StartCoroutine(LoadAsynchrounously(sceneIndex));
       
    }

    IEnumerator LoadAsynchrounously (int sceneIndex)
    {
         AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
         
         loadingScreen.SetActive(true);

         while (!operation.isDone)
         {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            
            slider.value = progress;
            
            yield return null;
         }
    }
}
