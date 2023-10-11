using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryMode : MonoBehaviour
{
   public void StoryButton(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
}
