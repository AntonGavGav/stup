using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PlayerLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject arm;
    private Animator clockAnimator;
    private float groundDistance = 0.4f;
    private int playerHealth = 100;
    public LayerMask groundMask;
    public HealthBar healthBar;
    
    private Vector3 velocity;
    private bool isGrounded;


    private void Start()
    {
        clockAnimator = arm.transform.GetComponent<Animator>();
        healthBar.SetMaxHealth(playerHealth);
    }

    private void FixedUpdate()
    {
        Gravity();
        Movement();
        ShowClock();
    }

    void Movement()
    {
       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");
       Vector3 move = transform.right * x + transform.forward * z;
       characterController.Move(move * speed * Time.deltaTime);
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move((velocity * Time.deltaTime));
    }

    void ShowClock()
    {
        if (Input.GetKey(KeyCode.C))
        {
            arm.SetActive(true);
            clockAnimator.SetBool("ShowClock", true);   
        }
        else
        {
            clockAnimator.SetBool("ShowClock", false);
        }
    }

    public void ArmSetActiveFalse()
    {
        arm.SetActive(false);
    }

    public void GetDamage(int damage)
    {
        playerHealth -= damage;
        healthBar.SetHealth(playerHealth);
    }
}
