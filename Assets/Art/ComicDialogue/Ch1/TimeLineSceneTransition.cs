using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class TimeLineSceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public string Chapter1; // Name of the scene to load after the timeline finishes playing
    private PlayableDirector playableDirector;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        playableDirector.stopped += OnTimelineStopped;
    }

    private void OnDestroy()
    {
        playableDirector.stopped -= OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        // Load the next scene when the timeline finishes playing
        SceneManager.LoadScene(Chapter1);
    }
}
