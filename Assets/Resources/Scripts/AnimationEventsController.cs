using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsController : MonoBehaviour
{
    [SerializeField] private GameObject LeftArm;
    private Animator leftHandAnimator;
    public Arms _arms;
    public EnemyLogic enemyLogic;
    private void Start()
    {
        leftHandAnimator = LeftArm.transform.GetComponent<Animator>();
    }

    public void AnimateTakePigeon()
    {
        enemyLogic.Take();
    }

    public void AnimatePlacePigeon()
    {
        enemyLogic.Place();
    }

    public void EndOfClockAnim()
    {
        LeftArm.SetActive(false);
        _arms.isWatchingTime = false;

        leftHandAnimator.enabled = false;
    }
    
}
