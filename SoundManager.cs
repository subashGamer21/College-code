using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform.
    public float maxDistance = 10f;    // The maximum distance at which the sound is audible.

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Calculate the distance between the player and the sound source.
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Check if the player is within the maximum audible distance.
        if (distanceToPlayer <= maxDistance)
        {
            // If the player is within range, play the sound.
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // If the player is outside the range, stop the sound.
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
