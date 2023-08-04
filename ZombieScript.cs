using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public float detectionRange = 10f; 
    public float attackRange = 1f; 

    private Transform player; 
    private NavMeshAgent agent; 
    private Animator animator; 

    private bool isPlayerInRange; 
    private playerHealth playerHealth;
    private bool isPlayerAlive = true;
    public CapsuleCollider collider;

    private void Awake()
    {
        // Assuming the player has a tag called "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<playerHealth>();
        collider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        if (!isPlayerAlive)
        {
            
            StopAttack();
            StopChase();
            return;
        }
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
        if (distanceToPlayer <= detectionRange)
        {
           
            ChasePlayer();

            
            if (distanceToPlayer <= attackRange)
            {
               
                AttackPlayer();
            }
            else
            {
                
                StopAttack();
            }
        }
        else
        {
           
            StopChase();
            StopAttack();
        }
    }

    private void ChasePlayer()
    {
        
        agent.SetDestination(player.position);
        animator.SetBool("Chase", true);
    }

    private void AttackPlayer()
    {
        
        agent.isStopped = true;

       
        animator.SetBool("Attack", true);
       
        // Reduce player health
        playerHealth playerHealth = player.GetComponent<playerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ReduceHealth(0.1f); // Adjust the value as needed
        }
    }

    public void StopAttack()
    {
       
        agent.isStopped = false;

       
        animator.SetBool("Attack", false);
        
        
    }

    public void StopChase()
    {
        animator.SetBool("Chase", false);
       
        agent.isStopped = true;
    }
    public void PlayerDied()
    {
        isPlayerAlive = false;
    }
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
