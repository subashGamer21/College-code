using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using Cinemachine;

public class playerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Scrollbar healthScrollbar;
    public Animator anim;
    private CharacterController playerCollider;
    public ThirdPersonShooterController third;
    private ThirdPersonController thirdcontroller;
    public CinemachineVirtualCamera followcamera;
    public CinemachineVirtualCamera deatthcam;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        playerCollider = GetComponent<CharacterController>();
        third = GetComponent<ThirdPersonShooterController>();
        third.enabled = true;
        thirdcontroller = GetComponent<ThirdPersonController>();
    }

    public void ReduceHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            // Player is dead, perform game over actions
            Die();
        }
        else
        {
            // Update health scrollbar value
            UpdateHealthUI();
        }
    }

    private void UpdateHealthUI()
    {
        // Update the value of the health scrollbar
        if (healthScrollbar != null)
        {
            float normalizedHealth = currentHealth / maxHealth;
            healthScrollbar.size = normalizedHealth;
        }
    }

    public void Die()
    {
        // Game over logic here
        Debug.Log("Player is dead");
        anim.SetTrigger("Death");
        ZombieScript zombie = FindObjectOfType<ZombieScript>();
        if (zombie != null)
        {
            zombie.PlayerDied();
        }
        playerCollider.enabled = false;
        zombie.collider.enabled = false;
        third.enabled = false;
        pickupcontroller controller = GetComponent<pickupcontroller>();
        controller.DropGun();
        thirdcontroller.enabled = false;
        deatthcam.enabled = true;
        followcamera.enabled = false;
    }

    // This method is called when the player collides with a medkit GameObject
    public void RefillHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthUI();
    }
}
