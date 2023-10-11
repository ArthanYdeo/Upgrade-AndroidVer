using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class LogoutManager : MonoBehaviour
{

    public string registrationSceneName = "Registration";
    private FirebaseAuth auth;
    void Start()
{
    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;

        if (auth == null)
        {
            Debug.LogError("Firebase Authentication is not initialized.");
        }
    });
}
public void LogoutAndReturnToRegistrationScene()
{
    if (auth.CurrentUser != null)
    {
        auth.SignOut();
        Debug.Log("User logged out successfully.");
    }
    SceneManager.LoadScene(registrationSceneName);
}




}
