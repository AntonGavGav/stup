using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyLogic : MonoBehaviour
{
    private int enemyHealth = 100;
    private float timeForColorChanging = 0.15f;

    public HealthBar healthBar;
    private Constants.PgColor color;
    
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject damageText;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private Transform pigeonHolderTransform;

    private Animator animator;
    private NavMeshAgent agent;
    private bool isDead = false;
    private bool isTaken = false;
    private Renderer pigeonMaterial;
    private MaterialSet materialSet;
    
    
    private void Start()
    {
        color = (Constants.PgColor) Random.Range(0, 2);
        animator = transform.GetChild(0).GetComponent<Animator>();
        healthBar.SetMaxHealth(enemyHealth);
        agent = transform.GetComponent<NavMeshAgent>();
        pigeonMaterial = transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
        materialSet = Constants.colors[color];
        pigeonMaterial.sharedMaterial = materialSet.primary;
        name.text = Constants.PgNames[Random.Range(0, Constants.PgNames.Length)];
    }

    private void Update()
    {
        if (!isDead && !isTaken)
        {
            animator.SetFloat("Blend", agent.velocity.magnitude);
            if (Vector3.Distance(transform.position, playerTransform.position) < 2f)
            {
                animator.SetBool("Atack", true);
                agent.isStopped = true;
                Vector3 point = new Vector3(playerTransform.position.x, transform.position.y,
                    playerTransform.position.z);
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
        else if (isTaken && !isDead)
        {
            transform.position = pigeonHolderTransform.position;
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
        if (!isDead && !isTaken)
        {
            GameObject damageText1 = Instantiate(damageText, transform.position, Quaternion.identity);
            damageText1.transform.GetComponent<TextMeshPro>().text = damage.ToString();
            StartCoroutine("SwitchColors");
            enemyHealth -= damage;
            healthBar.SetHealth(enemyHealth);
            if (enemyHealth <= 0)
            {
                Death();
                transform.GetChild(0).GetChild(0).GetComponent<Renderer>().sharedMaterial = materialSet.secondary;
                //Destroy(gameObject);
                transform.GetChild(0).GetChild(0).GetComponent<Renderer>().sharedMaterial = materialSet.primary;
            }
        }
    }

    IEnumerator SwitchColors()
    {
        yield return new WaitForSeconds(0.05f);
        pigeonMaterial.sharedMaterial = materialSet.secondary;
        yield return new WaitForSeconds(timeForColorChanging);
        pigeonMaterial.sharedMaterial = materialSet.primary;
    }

    private void Death()
    {
        isDead = true;
        Destroy(animator);
        Destroy(agent);
        Destroy(transform.GetComponent<CapsuleCollider>());
        Destroy(transform.GetChild(0).GetComponent<PigeonAtack>());
        healthBar.HealthBarDestroy();
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        Rigidbody[] rigidbodies = gameObject.GetComponentsInChildren<Rigidbody>();
        foreach(Collider collider in colliders){
            collider.enabled = true;
        }
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(10f, transform.position, 5f, 3f, ForceMode.Impulse);;
        }
        Destroy(this);
    }

    public void Take()
    {
        transform.SetParent(pigeonHolderTransform.parent);
        isTaken = true;
        agent.enabled = false;
        transform.GetComponent<CapsuleCollider>().enabled = false;
        healthBar.TurnOffBillboard();
        transform.position = pigeonHolderTransform.position;
        transform.rotation = pigeonHolderTransform.rotation;
    }

    public void Place()
    {
        transform.SetParent(null);
        isTaken = false;
        agent.enabled = true;
        transform.GetComponent<CapsuleCollider>().enabled = true;
        healthBar.TurnOnBillboard();
    }
    
}
