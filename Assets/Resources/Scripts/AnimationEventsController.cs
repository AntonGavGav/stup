using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsController : MonoBehaviour
{
    [SerializeField] private GameObject LeftArm;
    private Animator leftHandAnimator;
    public Arms _arms;
    private PigeonLogic enemyLogic;
    private void Start()
    {
        leftHandAnimator = LeftArm.transform.GetComponent<Animator>();
    }

    public void AnimateTakePigeon()
    {
        enemyLogic = _arms.pigeonToBeHoldLogic;
        enemyLogic.Take();
    }

    public void AnimatePlacePigeon()
    {
        enemyLogic.Place();
        enemyLogic = null;
    }

    public void EndOfClockAnim()
    {
        LeftArm.SetActive(false);
        _arms.isWatchingTime = false;

        leftHandAnimator.enabled = false;
    }
}
