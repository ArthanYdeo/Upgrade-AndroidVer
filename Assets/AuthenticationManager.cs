using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.Networking;


public class AuthenticationManager : MonoBehaviour
{

    public GameObject successPanel;

    public Button registerButton;
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

    private const string metaMaskAppURI = "metamask://auth";


    public virtual void Start()
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

         successPanel.SetActive(false);
    }
 public void Register()
{
    // Disable the register button
    registerButton.interactable = false;

                    SwitchToSuccessPanel();
    string username = registerUsernameInput.text;
    string email = registerEmailInput.text;
    string password = registerPasswordInput.text;
    string wallet = registerWalletInput.text;

    // Check for empty fields
    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(wallet))
    {
        Debug.LogWarning("Please fill in all fields.");

        // Re-enable the register button
        // registerButton.interactable = true;

        return;
    }

    // Create user with email and password
    Debug.Log("Creating user...");
    auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
    {
        if (task.IsCanceled || task.IsFaulted)
        {
            Debug.LogError("Failed to register user: " + task.Exception);

            // Re-enable the register button
            // registerButton.interactable = true;

            return;
        }

        // Registration successful, get user's unique ID
        FirebaseUser newUser = task.Result.User;
        string userId = newUser.UserId;

        // Save additional user data to database
        var userData = new UserData(username, email, wallet);
        string userDataJson = JsonUtility.ToJson(userData);

        Debug.Log("Saving user data to database...");
        dbRef.Child("users").Child(userId).SetRawJsonValueAsync(userDataJson)
            .ContinueWith(writeTask =>
            {
                if (writeTask.IsFaulted)
                {
                    Debug.LogError("Failed to save user data: " + writeTask.Exception);

                    // Re-enable the register button
                    // registerButton.interactable = true;

                    return;
                }

                Debug.Log("User registered successfully.");


                // Re-enable the register button
                // registerButton.interactable = true;
            });
    });
}



    void SwitchToSuccessPanel()
{
    successPanel.SetActive(true);
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
            // Store the logged-in user
            LoggedInUser = newUser;

            // Update the indication after successful login

            // Debug the scene transition
            Debug.Log("Loading next scene...");
            SceneManager.LoadScene(sceneNameToLoad);
        }, TaskScheduler.FromCurrentSynchronizationContext());
}


    public void Logout()
    {
        if (auth != null)
        {
            auth.SignOut();
            Debug.Log("User signed out successfully.");
            SceneManager.LoadScene("Login");
        }
        else
        {
            Debug.LogWarning("Firebase authentication is not initialized.");
        }
    }


       public void InitiateMetaMaskAuthentication()
    {
        // Open MetaMask app using deep linking
        Application.OpenURL(metaMaskAppURI);
    }

    // Method to handle authentication response from MetaMask
    public void HandleMetaMaskAuthenticationResponse(string response)
    {
        // Parse and verify the authentication response
        // Example: response format - "ethereumAddress:signature"
        string[] parts = response.Split(':');
        if (parts.Length == 2)
        {
            string ethereumAddress = parts[0];
            string signature = parts[1];

            // Verify the signature and authenticate the user
            if (VerifyAuthenticationWithMetaMask(ethereumAddress, signature))
            {
                // Authentication successful
                Debug.Log("MetaMask authentication successful. User: " + ethereumAddress);
                // Grant access to the user
                GrantAccess(ethereumAddress);
            }
            else
            {
                // Authentication failed
                Debug.Log("MetaMask authentication failed.");
                // Handle authentication failure
                HandleAuthenticationFailure();
            }
        }
        else
        {
            // Invalid response format
            Debug.Log("Invalid MetaMask authentication response.");
            // Handle invalid response
            HandleAuthenticationFailure();
        }
    }

    // Method to verify authentication with MetaMask
    private bool VerifyAuthenticationWithMetaMask(string ethereumAddress, string signature)
    {
        return true; 
    }

    // Method to grant access to the user
    private void GrantAccess(string ethereumAddress)
    {

    }
    private void HandleAuthenticationFailure()
    {
        // Handle authentication failure, display error message, etc.
    }

     private bool CheckRegistrationStatus()
    {
        return PlayerPrefs.HasKey("Registered");
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