using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SkipTimeline : MonoBehaviour
{
    public PlayableDirector playableDirector; // Reference to the PlayableDirector controlling the Timeline
    public double skipTime = 5.0; // Time to skip to in seconds

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(SkipTimelines);
    }

    void SkipTimelines()
    {
        // Set the time to skip to
        playableDirector.time = skipTime;

        // Ensure that the timeline is playing after skipping (optional)
        playableDirector.Play();
    }
}
