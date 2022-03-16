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

    private NavMeshAgent agent;

    private void Start()
    {
        healthBar.SetMaxHealth(enemyHealth);
        agent = transform.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var playerTransformPosition = playerTransform.position;
        agent.SetDestination(playerTransformPosition);
        //Vector3 point = new Vector3(playerTransformPosition.x, transform.position.y, playerTransformPosition.z);
        //transform.LookAt(point);
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
