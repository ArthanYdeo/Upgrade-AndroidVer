using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class UserEmailDisplay : MonoBehaviour
{
    public Text emailText;
    private FirebaseAuth auth;

void Start()
{
    FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
    });
}
public void DisplayUserEmail()
{
    if (auth.CurrentUser != null)
    {
        string userEmail = auth.CurrentUser.Email;
        emailText.text = "Email: " + userEmail;
         FindObjectOfType<UserEmailDisplay>().DisplayUserEmail();
    }
    else
    {
        emailText.text = "User not logged in";
    }
}
}
