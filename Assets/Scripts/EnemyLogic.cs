using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    private int enemyHealth = 100;

    public HealthBar healthBar;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator animator;

    private NavMeshAgent agent;

    private void Start()
    {
        healthBar.SetMaxHealth(enemyHealth);
        agent = transform.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        animator.SetFloat("Blend", agent.velocity.magnitude);
        if (Vector3.Distance(transform.position, playerTransform.position) < 5.5f)
        {
            agent.isStopped = true;
            Vector3 point = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
            SmoothlyRotateTowardsObj(playerTransform, 0.01f);
            //transform.LookAt(point);
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(playerTransform.position); 
        }
    }

    private void SmoothlyRotateTowardsObj(Transform target, float speed)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rotationGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, speed); 
    }
    public void ApplyDamage(int damage)
    {
        enemyHealth -= damage;
        healthBar.SetHealth(enemyHealth);
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
