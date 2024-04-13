using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
public class LoggedInFetchUser : MonoBehaviour
{
    private FirebaseAuth auth;
    private DatabaseReference dbRef;

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

            // Check if a user is logged in
            if (auth.CurrentUser != null)
            {
                // Fetch the currently logged-in user and their data from the database
                FetchUserDataFromDatabase(auth.CurrentUser.UserId);
            }
            else
            {
                Debug.LogWarning("No user is currently logged in.");
            }
        }
        else
        {
            Debug.LogError("Failed to initialize Firebase: " + task.Result.ToString());
        }
    });
}


    private void FetchUserDataFromDatabase(string userId)
    {
        dbRef.Child("users").Child(userId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to fetch user data: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot != null && snapshot.Exists)
                {
                    // Deserialize the user data
                    string userDataJson = snapshot.GetRawJsonValue();
                    UserData userData = JsonUtility.FromJson<UserData>(userDataJson);

                    // Output the fetched user data
                    Debug.Log("Fetched user data:");
                    Debug.Log("Username: " + userData.username);
                    Debug.Log("Email: " + userData.email);
                    Debug.Log("Wallet: " + userData.wallet);
                }
                else
                {
                    Debug.LogWarning("User data not found in the database.");
                }
            }
        });
    }

    [System.Serializable]
    public class UserData
    {
        public string username;
        public string email;
        public string wallet;
    }
}