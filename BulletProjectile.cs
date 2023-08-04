using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public int damageAmount = 20;
    [SerializeField] private Transform green; // Particle effect for hitting a zombie
    [SerializeField] private Transform VFxblood; // Particle effect for hitting something else
    public float speed;
    public float forceToAdd = 500f; // Force to add when hit
    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            // Hit a zombie
            ZombieHealth1 zombieHealth = other.GetComponent<ZombieHealth1>();
            girlhealth girlhealth = other.GetComponent<girlhealth>();
            if (zombieHealth != null)
            {
                zombieHealth.TakeDamage(damageAmount);
            }
            if (girlhealth != null)
            {
                girlhealth.TakeDamage(damageAmount);
            }

            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                // Apply force to the collided zombie
                Vector3 forceDirection = other.transform.position - transform.position;
                forceDirection.Normalize();
                otherRigidbody.AddForce(forceDirection * forceToAdd);
            }

            Instantiate(VFxblood, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
        }
        else
        {
            // Hit something else
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                // Apply force to the collided object
                Vector3 forceDirection = other.transform.position - transform.position;
                forceDirection.Normalize();
                otherRigidbody.AddForce(forceDirection * forceToAdd);
            }

            Instantiate(green, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
