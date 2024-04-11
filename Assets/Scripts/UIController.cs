using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;

public class UIController : MonoBehaviour
{
    Player player;
    Text distanceText;

    GameObject results;
    Text finalDistanceText;

    private AuthenticationManager authManager;

    private void Awake()
    {
        player = GameObject.Find("Player")?.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player not found or Player script not attached.");
        }

        distanceText = GameObject.Find("DistanceText")?.GetComponent<Text>();
        if (distanceText == null)
        {
            Debug.LogError("DistanceText not found or Text component not attached.");
        }

        results = GameObject.Find("Results");
        if (results == null)
        {
            Debug.LogError("Results not found.");
        }

        finalDistanceText = GameObject.Find("FinalDistanceText")?.GetComponent<Text>();
        if (finalDistanceText == null)
        {
            Debug.LogError("FinalDistanceText not found or Text component not attached.");
        }

        if (results != null)
        {
            results.SetActive(false);
        }

        // Get the AuthenticationManager instance
        authManager = FindObjectOfType<AuthenticationManager>();
        if (authManager == null)
        {
            Debug.LogError("AuthenticationManager not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";

        if (player.isDead)
        {
            results.SetActive(true);
            finalDistanceText.text = distance + " m";

            // Save distance to Firebase Realtime Database
            if (AuthenticationManager.LoggedInUser != null)
            {
                string userId = AuthenticationManager.LoggedInUser.UserId;
                FirebaseDatabase.DefaultInstance.GetReference("users").Child(userId).Child("arcadeDistance").SetValueAsync(distance);
            }
        }
    }
}
