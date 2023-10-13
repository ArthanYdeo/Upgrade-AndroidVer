using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class LogoutManager : MonoBehaviour
{

    public string titleSceneName = "Title";
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
public void LogoutAndReturnTotitleScene()
{
    if (auth.CurrentUser != null)
    {
        auth.SignOut();
        Debug.Log("User logged out successfully.");
    }
    SceneManager.LoadScene(titleSceneName);
}




}
