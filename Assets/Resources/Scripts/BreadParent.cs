using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Resources.Scripts;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class BreadParent : MonoBehaviour, IconProvider, ITakeable, ISelectable
{
  
    [SerializeField] private Sprite imageOnSlot;

    [SerializeField]
    private Transform instantiateBreadTransform;

    private Bread bread;
    private bool isNotDeselected = false;
    private Transform inHandsDefualtTransform;


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
        animator = transform.GetChild(0).GetComponent<Animator>();
        bread = transform.GetChild(0).GetComponent<Bread>();
        bread.AnimationEnded += ChangeState;
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
                print("ok");
                break;
            case State.NotInUse:
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<CapsuleCollider>().enabled = true;
                break;
        }
    }
    
    private void ChangeState()
    {
        state = State.ReadyToAtack;
        animator.Play("BreadReturnToDefualtPos");
    }

    public void Select()
    {
        gameObject.SetActive(true);
    }

    public void Deselect()
    {
        ChangeState();
        gameObject.SetActive(false);
    }

    public Sprite GetItemSprite()
    {
        return imageOnSlot;
    }

    public void Take()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<OutlineOnComeUp>().enabled = false;
        transform.GetChild(0).GetComponent<Outline>().enabled = false;
        transform.SetParent(instantiateBreadTransform );
        transform.localScale = new Vector3(1, 1, 1);
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        animator.enabled = true;
        inHandsDefualtTransform = transform.GetChild(0).transform;
        state = State.ReadyToAtack;
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
        if((state != State.ReadyToAtack || state!= State.Atacking) && Vector3.Distance(transform.position, armsTransform.position )< 3f){
            return true;
        }

        return false;
    }
    
}
