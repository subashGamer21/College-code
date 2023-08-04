using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineSceneChanger : MonoBehaviour
{
    private PlayableDirector timeline;
    public string scenename;

    private void Start()
    {
        timeline = GetComponent<PlayableDirector>();
        timeline.stopped += OnTimelineFinished;
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // Load the new scene when the Timeline finishes
        SceneManager.LoadScene(scenename);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid any potential memory leaks
        timeline.stopped -= OnTimelineFinished;
    }
}
