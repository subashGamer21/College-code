using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class class_trigger : MonoBehaviour
{
    public Transform teleportLocation; // Assign the target location in the Inspector
    public float fadeDuration = 2.0f; // Duration of the fade-in/out animation
    public float textDisplayDuration = 10.0f;

    private Image fadeImage;
    private bool isTeleporting = false;
    public GameObject[] zombies;
    public Text searchcalss;
    public GameObject[] text_collider;
    public Text findNishan;
    public GameObject second_particle;
    public GameObject third_particle;
    
    //private int activeZombieCount;
    

    // Start is called before the first frame update
    void Start()
    {
        third_particle.SetActive(false);
        findNishan.enabled = false;
        foreach (GameObject zombie in zombies)
        {
            zombie.SetActive(false);
        }
        searchcalss.enabled = false;
       // completionText.enabled = false;
       // activeZombieCount = zombies.Length;
        // Create the fade canvas and image dynamically
        GameObject fadeCanvasObj = new GameObject("FadeCanvas");
        fadeCanvasObj.transform.SetParent(transform);
        RectTransform fadeCanvasRect = fadeCanvasObj.AddComponent<RectTransform>();
        fadeCanvasRect.anchorMin = Vector2.zero;
        fadeCanvasRect.anchorMax = Vector2.one;
        fadeCanvasRect.offsetMin = Vector2.zero;
        fadeCanvasRect.offsetMax = Vector2.zero;
        fadeCanvasObj.layer = LayerMask.NameToLayer("UI");

        Canvas fadeCanvas = fadeCanvasObj.AddComponent<Canvas>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        fadeCanvas.sortingOrder = 999; // Make sure it's on top of everything

        CanvasScaler canvasScaler = fadeCanvasObj.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080); // Adjust as needed

        fadeCanvasObj.AddComponent<GraphicRaycaster>();

        GameObject fadeImageObj = new GameObject("FadeImage");
        fadeImageObj.transform.SetParent(fadeCanvasRect);
        RectTransform fadeImageRect = fadeImageObj.AddComponent<RectTransform>();
        fadeImageRect.anchorMin = Vector2.zero;
        fadeImageRect.anchorMax = Vector2.one;
        fadeImageRect.offsetMin = Vector2.zero;
        fadeImageRect.offsetMax = Vector2.zero;

        fadeImage = fadeImageObj.AddComponent<Image>();
        fadeImage.color = Color.clear;


        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            third_particle.SetActive(true);
            second_particle.SetActive(false);
            foreach (GameObject zombie in zombies)
            {
                zombie.SetActive(true);
            }
            isTeleporting = true;
            searchcalss.enabled = true;
            foreach(GameObject collider in text_collider)
            {
                collider.SetActive(false);
            }

    // Disable all scripts on the player
    MonoBehaviour[] playerScripts = other.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in playerScripts)
            {
                if (script != this) // Skip disabling the current script (class_trigger)
                    script.enabled = false;
            }

            // Start the teleportation coroutine with fade
            StartCoroutine(TeleportAndEnableScripts(other.gameObject));
        }
    }

    private IEnumerator TeleportAndEnableScripts(GameObject player)
    {
        // Fade Out the scene
        float startTime = Time.time;
        Color startColor = Color.clear;
        Color targetColor = Color.black;

        while (Time.time < startTime + fadeDuration)
        {
            float normalizedTime = (Time.time - startTime) / fadeDuration;
            fadeImage.color = Color.Lerp(startColor, targetColor, normalizedTime);
            yield return null;
        }

        fadeImage.color = targetColor;

        // Teleport the player to the new location
        player.transform.position = teleportLocation.position;
        player.transform.rotation = teleportLocation.rotation;

        // Wait for another 1 second (or any desired time)
        yield return new WaitForSeconds(0.8f);

        // Fade In the scene
        startTime = Time.time;
        startColor = targetColor;
        targetColor = Color.clear;

        while (Time.time < startTime + fadeDuration)
        {
            float normalizedTime = (Time.time - startTime) / fadeDuration;
            fadeImage.color = Color.Lerp(startColor, targetColor, normalizedTime);
            yield return null;
        }

        fadeImage.color = targetColor;

        // Re-enable all the scripts on the player
        MonoBehaviour[] playerScripts = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in playerScripts)
        {
            if (script != this) // Skip enabling the current script (class_trigger)
                script.enabled = true;
        }

        isTeleporting = false;
    }

}

