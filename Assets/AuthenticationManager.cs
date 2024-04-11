using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class AuthenticationManager : MonoBehaviour
{
    public string sceneNameToLoad;
    public TMP_InputField registerUsernameInput;
    public TMP_InputField registerEmailInput;
    public TMP_InputField registerPasswordInput;
    public TMP_InputField registerWalletInput;

    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;

    private FirebaseAuth auth;
    private DatabaseReference dbRef;

    public static Firebase.Auth.FirebaseUser LoggedInUser { get; private set; }


    private void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                // Firebase is ready
                auth = FirebaseAuth.DefaultInstance;
                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                Debug.LogError("Failed to initialize Firebase: " + task.Result.ToString());
            }
        });
    }

    public void Register()
    {
        string username = registerUsernameInput.text;
        string email = registerEmailInput.text;
        string password = registerPasswordInput.text;
        string wallet = registerWalletInput.text;

        // Check for empty fields
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(wallet))
        {
            Debug.LogWarning("Please fill in all fields.");
            return;
        }

        // Create user with email and password
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Failed to register user: " + task.Exception);
                return;
            }

            // Registration successful, get user's unique ID
            Firebase.Auth.FirebaseUser newUser = task.Result.User;
            string userId = newUser.UserId;

            // Save additional user data to database
            var userData = new UserData(username, email, wallet);
             string userDataJson = JsonUtility.ToJson(userData);
            dbRef.Child("users").Child(userId).SetRawJsonValueAsync(userDataJson)
            .ContinueWith(writeTask =>
            {
                if (writeTask.IsFaulted || writeTask.IsCanceled)
                {
                    Debug.LogError("Failed to write user data to database: " + writeTask.Exception);
                }
                else
                {
                    Debug.Log("User registered successfully!");
                }
            });
    });
}

     public void Login()
    {
        string email = loginEmailInput.text;
        string password = loginPasswordInput.text;

        Debug.Log("Clicked Signin");

        Firebase.Auth.FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(
            email, password).ContinueWith(task =>
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
                    // Handle failure
                    return;
                }

                // Access the FirebaseUser from the AuthResult
                Firebase.Auth.AuthResult authResult = task.Result;
                Firebase.Auth.FirebaseUser newUser = authResult.User;

                Debug.LogFormat("Signed in Successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);

                // Store the logged-in user
                LoggedInUser = newUser;

                // Debug the scene transition
                Debug.Log("Loading next scene...");
                SceneManager.LoadScene(sceneNameToLoad);
            }, TaskScheduler.FromCurrentSynchronizationContext());
    }


    [System.Serializable]
    public class UserData
    {
        public string username;
        public string email;
        public string wallet;

        public UserData(string username, string email, string wallet)
        {
            this.username = username;
            this.email = email;
            this.wallet = wallet;
        }
    }
}
