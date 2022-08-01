using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using UnityEngine;
using Random = System.Random;

public class BreadWeapon : MonoBehaviour, IconProvider, ITakeable
{
    private Weapon weapon = new Weapon("Bread", 20f, 0f);
    [SerializeField] private Sprite imageOnSlot;



    public enum State
    {
        ReadyToAtack,
        Atacking,
        NotInUse
    }

    public State state;
    private Animator animator;

    

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
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
                        hitCollider.SendMessage("ApplyDamage", UnityEngine.Random.Range(5, (int)weapon.damage), SendMessageOptions.DontRequireReceiver);
                    }
                }
                break;
            case  State.Atacking:
                break;
            case State.NotInUse:
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<BoxCollider>().enabled = true;
                break;
        }
    }
    
    public void ChangeState()
    {
        state = State.ReadyToAtack;
    }

    private void TakeBread()
    {
        
    }
    public Sprite GetItemSprite()
    {
        Debug.Log("Taken");
        return imageOnSlot;
    }

    public void Take()
    {
        Debug.Log("IsTaken");
    }

    public void Place()
    {
    }

    public GameObject returnObject()
    {
        return gameObject;
    }

    public bool IsReadyToBeHold(Transform armsTransform)
    {
        return true;
    }
    
}
