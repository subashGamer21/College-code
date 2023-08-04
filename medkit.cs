using UnityEngine;

public class medkit : MonoBehaviour
{
    public float healthAmount = 50f; // The amount of health to refill

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the playerHealth script on the player object
            playerHealth playerHealthScript = other.GetComponent<playerHealth>();

            if (playerHealthScript != null)
            {
                // Refill the player's health
                playerHealthScript.RefillHealth(healthAmount);

                // Destroy the medkit GameObject after it's used
                Destroy(gameObject);
            }
        }
    }
}
