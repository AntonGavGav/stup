using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    private GameObject LeftArm;
    private GameObject RightArm;
    private Animator leftHandAnimator;
    private Animator rightHandAnimator;
    private bool isPigeonInHands = false;
    public bool isWatchingTime = false;

    private void Start()
    {
        LeftArm = transform.GetChild(0).gameObject;
        RightArm = transform.GetChild(1).gameObject;
        leftHandAnimator = LeftArm.transform.GetComponent<Animator>();
        rightHandAnimator = RightArm.transform.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        ShowClock();
    }

    private void Update()
    {
        TakePigeon();
    }


    private void ShowClock()
    {
        if (Input.GetKey(KeyCode.C) && !isPigeonInHands)
        {
            LeftArm.SetActive(true);
            leftHandAnimator.enabled = true;
            leftHandAnimator.SetBool("ShowClock", true);
            isWatchingTime = true;
        }
        else
        {
            leftHandAnimator.SetBool("ShowClock", false);
        }
    }

    private void TakePigeon()
    {
        
        if (Input.GetKeyDown(KeyCode.H) && !isPigeonInHands && !isWatchingTime)
        {
            LeftArm.SetActive(true);
            leftHandAnimator.enabled = true;
            rightHandAnimator.SetBool("HoldPigeon", true);
            leftHandAnimator.SetBool("HoldPigeon", true);
            isPigeonInHands = true;
        }
        else if(Input.GetKeyDown(KeyCode.H) && isPigeonInHands && !isWatchingTime)
        {
            rightHandAnimator.SetBool("HoldPigeon", false);
            leftHandAnimator.SetBool("HoldPigeon", false);
            isPigeonInHands = false;
        } 
    }
    

    
}
