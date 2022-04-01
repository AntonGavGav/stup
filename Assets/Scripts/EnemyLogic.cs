using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyLogic : MonoBehaviour
{
    private int enemyHealth = 100;
    private float timeForColorChanging = 0.15f;

    public HealthBar healthBar;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator animator;
    [FormerlySerializedAs("spriteRender")] [SerializeField] private Material material;

    private NavMeshAgent agent;
    private Color defaultColor;
    
    private void Start()
    {
        defaultColor = material.color;
        
        healthBar.SetMaxHealth(enemyHealth);
        agent = transform.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        animator.SetFloat("Blend", agent.velocity.magnitude);
        if (Vector3.Distance(transform.position, playerTransform.position) < 2f)
        {
            animator.SetBool("Atack", true);
            agent.isStopped = true;
            Vector3 point = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
            SmoothlyRotateTowardsObj(playerTransform, 0.01f);
            //transform.LookAt(point);
        }
        else
        {
            animator.SetBool("Atack", false);
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
        StartCoroutine("SwitchColors");
        enemyHealth -= damage;
        healthBar.SetHealth(enemyHealth);
        if (enemyHealth <= 0)
        {
            material.color = new Color(1f, 0.30196078f, 0.30196078f);
            Destroy(gameObject);
            material.color = defaultColor;
        }
    }

    IEnumerator SwitchColors()
    {
        yield return new WaitForSeconds(0.05f);
        material.color = new Color(1f, 0.30196078f, 0.30196078f);
        yield return new WaitForSeconds(timeForColorChanging);
        material.color = defaultColor;
    }
}
