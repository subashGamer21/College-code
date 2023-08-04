using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class loading_next : MonoBehaviour
{
    private PlayableDirector timeline;

    private void Start()
    {
        timeline = GetComponent<PlayableDirector>();
        timeline.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // Load the new scene when the Timeline finishes
        SceneManager.LoadScene("Cutscene 1");
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid any potential memory leaks
        timeline.stopped -= OnTimelineFinished;
    }

   
}

   


