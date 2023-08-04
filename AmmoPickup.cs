using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the player's rifle ammo script
            ThirdPersonShooterController rifleAmmo = other.GetComponent<ThirdPersonShooterController>();

            if (rifleAmmo != null)
            {
                // Add ammo to the player's rifle
                rifleAmmo.AddAmmo(ammoAmount);

                // Destroy the ammo pickup object
                Destroy(gameObject);
            }
        }
    }
}
