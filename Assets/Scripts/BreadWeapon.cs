using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BreadWeapon : MonoBehaviour
{
    public enum State
    {
        ReadyToAtack,
        Atacking
    }

    public State state;
    private Animator animator;
    private void Start()
    {
        animator = transform.GetComponent<Animator>();
        state = State.ReadyToAtack;
    }

    private void Update()
    {
        switch (state)
        {
            case State.ReadyToAtack:
                if (Input.GetMouseButtonDown(0))
                {
                    state = State.Atacking;
                    animator.SetTrigger("Attack");
                    Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);
                    foreach (var hitCollider in hitColliders)
                    {
                        hitCollider.SendMessage("ApplyDamage", UnityEngine.Random.Range(5, 20), SendMessageOptions.DontRequireReceiver);
                    }
                }
                break;
            case  State.Atacking:
                break;
        }
    }
    
    public void ChangeState()
    {
        state = State.ReadyToAtack;
    }
}
