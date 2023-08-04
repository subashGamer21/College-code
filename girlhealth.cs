using System.Collections;
using UnityEngine;

public class girlhealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;
    public GameObject deathEffect;
    
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead)
            return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        MonoBehaviour[] scripts = GetComponentsInChildren<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = false;
        }
        isDead = true;

        // Play the death animation if there is an animator
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
       
        // Instantiate the death effect if provided
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Disable the zombie's collider and other components
        Collider zombieCollider = GetComponent<Collider>();
        if (zombieCollider != null)
        {
            zombieCollider.enabled = false;
        }

        
         Destroy(gameObject, 10f);
    }
}
