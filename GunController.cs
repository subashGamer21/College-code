using UnityEngine;

public class GunController : MonoBehaviour
{
    // Reference to the player or character controller
    public pickupcontroller characterController;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.gameObject == characterController.gameObject)
        {
            // Display the pickup message
            UIManager.Instance.DisplayPickupMessage(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.gameObject == characterController.gameObject)
        {
            // Hide the pickup message
            UIManager.Instance.DisplayPickupMessage(false);
        }
    }
}
