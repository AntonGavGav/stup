using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    private int enemyHealth = 100;

    public HealthBar healthBar;

    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        healthBar.SetMaxHealth(enemyHealth);
    }

    private void Update()
    {
        Vector3 point = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.LookAt(point);
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
