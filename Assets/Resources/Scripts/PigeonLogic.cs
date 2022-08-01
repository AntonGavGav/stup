using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PigeonLogic : MonoBehaviour, ITakeable
{
    private int enemyHealth;
    private float pigeonSpeed;
    private int pigeonRunningTriggerDistance = 6;
    private float timeForColorChanging = 0.15f;
    
    private Constants.PgColor color;
    
    [Header("EasierToInsertObjects")]
    public HealthBar healthBar;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject damageText;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private Transform pigeonHolderTransform;
    [SerializeField] private Outline outline;
    [SerializeField] private GameObject featherParticle;
    [SerializeField] private GameObject pigeonTakeTip;

    [Space(10)] [Header("Variables")] 
    [Range(1, 500)] [SerializeField] float walkRadius;
    [Range(0.1f, 2f)] [SerializeField] private float runningTolerance;
    [Range(1, 500)] [SerializeField] private float runningDistance;
    
    private Animator animator;
    private Animator pigeonTakeTipAnimator;
    private NavMeshAgent agent;
    private bool isDead = false;
    private Renderer pigeonMaterial;
    private MaterialSet materialSet;
    private float timeRemaining = 4;
    
    public enum State
    {
        Wandering,
        GoingToPlayer,
        RunningFromPlayer,
        Idle,
        InHands,
        Dead
    }

    public State state = State.Wandering;
    
    private void Start()
    {
        pigeonTakeTipAnimator = pigeonTakeTip.GetComponent<Animator>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        agent = transform.GetComponent<NavMeshAgent>();
        pigeonMaterial = transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
        outline.enabled = false;
        SetDifferenceOnStart();
    }

    private void Update()
    {
        
            switch (state)
            {
                case State.Wandering:
                    agent.speed = pigeonSpeed;
                    animator.SetFloat("Blend", agent.velocity.magnitude);
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        agent.SetDestination(RandomNavMeshLocation());
                        state = SetRandomState(); 
                    }
                    break;
                case State.GoingToPlayer:
                    break;
                case State.RunningFromPlayer:
                    agent.speed = pigeonSpeed * 2;
                    animator.SetFloat("Blend", agent.velocity.magnitude);
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (Vector3.Distance(transform.position, playerTransform.position) < pigeonRunningTriggerDistance)
                        {
                            agent.SetDestination(RandomNavMeshRunningLocation());
                        }
                        else
                        {
                            state = SetRandomState();
                        }
                    }
                    break;
                case State.Idle:
                    agent.speed = 0f;
                    animator.SetFloat("Blend", agent.velocity.magnitude);
                    StartCoroutine(SetRandomStateAfterTime(Random.Range(5f, 25f)));
                    break;
                case State.InHands:
                    transform.position = pigeonHolderTransform.position;
                    break;
                case State.Dead:
                    break;
            }
        

    }

    private void OnMouseOver()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) < 3f)
        {
            outline.enabled = true;
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else if(timeRemaining <= 0 && pigeonTakeTip != null)
            {
                pigeonTakeTip.SetActive(true);
                pigeonTakeTip = null;
            }
        }
        else
        {
            outline.enabled = false;
        }

    }

    private void OnMouseExit()
    {
        outline.enabled = false;
        timeRemaining = 4;
    }
    
    private Vector3 RandomNavMeshRunningLocation()
    {
        Vector3 offset= transform.forward * runningDistance;
        Vector3 position = offset + new Vector3(Random.Range(-runningTolerance, runningTolerance), transform.position.y, Random.Range(-runningTolerance, runningTolerance));
        return position;
    }

    private Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    private void SmoothlyRotateTowardsObj(Transform target, float speed)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rotationGoal = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationGoal, speed); 
    }

    private State SetRandomState()
    {
        if (Random.Range(0,3) == 0)
        {
            state = State.Idle;
        }
        else
        {
            state = State.Wandering;
        }
        return state;
    }
    IEnumerator SetRandomStateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (state != State.InHands)
        {
            if (state == State.RunningFromPlayer)
            {
                state = State.RunningFromPlayer;
            }
            else
            {
                state = State.Wandering;
            }
        }
        yield return new WaitForSeconds(time);
    }

    private void SetDifferenceOnStart()
    {
        enemyHealth = Random.Range(50, 150);
        color = (Constants.PgColor) Random.Range(0, 2);
        healthBar.SetMaxHealth(enemyHealth);
        name.text = Constants.PgNames[Random.Range(0, Constants.PgNames.Length)];
        materialSet = Constants.colors[color];
        pigeonMaterial.sharedMaterial = materialSet.primary;
        agent.speed = Random.Range(2.6f, 5f);
        pigeonSpeed = agent.speed;
        agent.acceleration = Random.Range(11f, 13f);
        agent.angularSpeed = Random.Range(120f, 400f);
        float scale = Random.Range(0.55f, 0.6f);
        transform.localScale = new Vector3(scale,scale,scale);
    }

    public void ApplyDamage(int damage)
    {
        if (!isDead && state != State.InHands)
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
        FindObjectOfType<Arms>().GetComponent<Arms>().AnimateHandsTake();
    }

    public void Place()
    {
        FindObjectOfType<Arms>().GetComponent<Arms>().AnimateHandsPlace();
    }
    
    public GameObject returnObject()
    {
        return gameObject;
    }

    public bool IsReadyToBeHold(Transform armsTransform)
    {
        if(state != PigeonLogic.State.InHands && Vector3.Distance(transform.position, armsTransform.position )< 3f){
            return true;
        }

        return false;
    }

    public void GraficalPlace()
    {
        transform.SetParent(null);
        agent.enabled = true;
        transform.GetComponent<CapsuleCollider>().enabled = true;
        healthBar.TurnOnBillboard();
        animator.SetBool("InHands", false);
        state = State.RunningFromPlayer;
    }
    public void GraficalTake()
    {
        animator.SetBool("InHands", true);
        transform.SetParent(pigeonHolderTransform.parent);
        agent.enabled = false;
        transform.GetComponent<CapsuleCollider>().enabled = false;
        healthBar.TurnOffBillboard();
        transform.position = pigeonHolderTransform.position;
        transform.rotation = pigeonHolderTransform.rotation;
        Instantiate(featherParticle, transform.GetChild(0).position, Quaternion.identity);
        state = State.InHands;
        if (pigeonTakeTipAnimator != null)
        {
            pigeonTakeTipAnimator.SetTrigger("Outro");
            pigeonTakeTipAnimator = null;
        }
    }
    
}
