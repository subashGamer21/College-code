using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Reference to the UI Text element
    public Text pickupMessageText;

    // Singleton instance
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of UIManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayPickupMessage(bool display)
    {
        // Enable or disable the pickup message text
        pickupMessageText.gameObject.SetActive(display);
    }
}
