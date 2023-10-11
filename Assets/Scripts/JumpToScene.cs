using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToScene : MonoBehaviour
{
   public string sceneNameToLoad;

    // Call this method to jump to the specified scene
    public void JumpToSceneName()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
