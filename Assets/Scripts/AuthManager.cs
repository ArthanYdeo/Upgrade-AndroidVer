using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
public class AuthManager : MonoBehaviour
{

    public GameObject successPanel;
    public GameObject alreadyexistPanel;
    public GameObject failedPanel;
    public Button registerButton, loginButton;
    private FirebaseAuth auth;
    public InputField email, password;

    public virtual void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
              FirebaseApp app = FirebaseApp.DefaultInstance;
             auth = FirebaseAuth.DefaultInstance;
            DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Firebase is ready
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    public void OnClickSignIn()
    {
        Debug.Log("Clicked Signin");
        Firebase.Auth.FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(
            email.text, password.text).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Signin Canceled");
                    // Handle cancellation
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log("Signin Failed");
                    SwitchToFailedPanel();
                    // Handle failure
                    return;
                }

                // Access the FirebaseUser from the AuthResult
                Firebase.Auth.AuthResult authResult = task.Result;
                Firebase.Auth.FirebaseUser newUser = authResult.User;

                Debug.LogFormat("Signed in Successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);    
            });
    }

    public void OnClickSignUp()
    {
        Debug.Log("Clicked Signup");
        Firebase.Auth.FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(
            email.text, password.text).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Signup Canceled");
                    // Handle cancellation
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log("Signup Failed");
                     SwitchToalreadyexistPanel();
                    // Handle failure
                    return;
                }

                // Access the FirebaseUser from the AuthResult
                Firebase.Auth.AuthResult authResult = task.Result;
                Firebase.Auth.FirebaseUser newUser = authResult.User;

                Debug.LogFormat("Signed up Successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                    SwitchToSuccessPanel();
                    
                
            });
    }

    private void SwitchToSuccessPanel()
{
    successPanel.SetActive(true);
}
  private void SwitchToFailedPanel()
{
    failedPanel.SetActive(true);
}
 private void SwitchToalreadyexistPanel()
{
    alreadyexistPanel.SetActive(true);
}

}
