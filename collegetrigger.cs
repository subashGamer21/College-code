using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collegetrigger : MonoBehaviour
{
    public Transform teleportLocation;
    public float fadeDuration = 2.0f; // Duration of the fade-in/out animation
    private bool isTeleporting = false;
    public Text exitclass;
    private Image fadeImage;
    public GameObject show_Zombie;
    // Start is called before the first frame update
    void Start()
    {
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
        show_Zombie.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            exitclass.enabled = false;
        }

        if (other.CompareTag("Player") && !isTeleporting)
        {
           
            isTeleporting = true;
            show_Zombie.SetActive(true);
           

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

