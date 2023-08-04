using UnityEngine;

public class ZombieHealth1 : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
   
    public float destroyDelay = 10f;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Deactivate all scripts on the zombie
        MonoBehaviour[] scripts = GetComponentsInChildren<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = false;
        }
        
        // Deactivate the animator
        animator.enabled = false;

        // Destroy the zombie object after the specified delay
        StartCoroutine(DestroyAfterDelay(destroyDelay));
    }

    private System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
