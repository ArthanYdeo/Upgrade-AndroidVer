using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public Transform leaderboardContainer; 
    public GameObject leaderboardEntryPrefab; 

    private DatabaseReference databaseReference;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase initialized successfully");
                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                FetchLeaderboardData();
            }
            else
            {
                Debug.LogError("Failed to initialize Firebase: " + task.Result.ToString());
            }
        });
    }

    private void FetchLeaderboardData()
    {
        databaseReference.Child("users").OrderByChild("arcadeDistance").LimitToLast(3).ValueChanged += HandleLeaderboardValueChanged;
    }


    private void HandleLeaderboardValueChanged(object sender, ValueChangedEventArgs args)
    {
      

        if (args.DatabaseError != null)
        {
            Debug.LogError("Database error: " + args.DatabaseError.Message);
            return;
        }

        if (args.Snapshot != null && args.Snapshot.ChildrenCount > 0)
        {
            List<DataSnapshot> userSnapshots = new List<DataSnapshot>(args.Snapshot.Children);
            userSnapshots.Reverse(); // Reverse the list to get the highest arcadeDistance at index 0

            int totalEntries = userSnapshots.Count;
            int rank = 1;
            foreach (DataSnapshot userSnapshot in userSnapshots)
            {
                try
                {
                    string username = userSnapshot.Child("username").Value.ToString();
                    int arcadeDistance = Convert.ToInt32(userSnapshot.Child("arcadeDistance").Value);
                    string wallet = userSnapshot.Child("wallet").Value.ToString();

                    DisplayLeaderboardEntry(rank, username, arcadeDistance, wallet, totalEntries);
                    rank++;
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error parsing leaderboard entry: " + ex.Message);
                }
            }
        }
        else
        {
            Debug.Log("No leaderboard data available.");
        }
    }
private List<GameObject> leaderboardEntries = new List<GameObject>(); // Maintain a list of leaderboard entries

private void DisplayLeaderboardEntry(int rank, string username, int arcadeDistance, string wallet, int totalEntries)
{
    GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardContainer);

    // Add the entry to the list
    leaderboardEntries.Add(entry);

    // Adjust the position of the new entry based on its index in the list
    RectTransform entryTransform = entry.GetComponent<RectTransform>();
    if (leaderboardEntries.Count > 1)
    {
        RectTransform lastEntryTransform = leaderboardEntries[leaderboardEntries.Count - 2].GetComponent<RectTransform>();
        entryTransform.anchoredPosition = new Vector2(entryTransform.anchoredPosition.x, lastEntryTransform.anchoredPosition.y - lastEntryTransform.sizeDelta.y);
    }

    Text rankText = entry.transform.Find("Rank").GetComponent<Text>();
    Text usernameText = entry.transform.Find("Username").GetComponent<Text>();
    Text distanceText = entry.transform.Find("ArcadeDistance").GetComponent<Text>();
    Text walletText = entry.transform.Find("Wallet").GetComponent<Text>();

    rankText.text = rank.ToString();
    usernameText.text = username;
    distanceText.text = arcadeDistance.ToString() + " m";
    walletText.text = wallet;
}
}

