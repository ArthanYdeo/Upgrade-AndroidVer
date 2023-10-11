using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
   public void TaptoStart()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
   }
   
}
