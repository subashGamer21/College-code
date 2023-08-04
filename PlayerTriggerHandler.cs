using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerTriggerHandler : MonoBehaviour
{
    public PlayableDirector timeline;

    public GameObject theplayer;
    public GameObject Cam;
    public Text[] first;
    public Canvas health;
    public GameObject subtitle;
    public GameObject sound;
    public GameObject emergency;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            theplayer.SetActive(false);
            Cam.SetActive(false);
            health.enabled = false;
            foreach( Text text in first)
            {
                text.enabled = false;
            }
            PlayTimeline();
        }
    }

    


    private void PlayTimeline()
    {
        subtitle.SetActive(true);
        sound.SetActive(false);
        emergency.SetActive(false);
;        if (timeline != null)
        {
            timeline.Play();
        }
    }
}
