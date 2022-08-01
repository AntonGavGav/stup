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
    public delegate void PlaceHandler();

    public delegate void TakeHandler();

    public event PlaceHandler Placed;
    public event TakeHandler Taken;
    private void Start()
    {
        if (leftHandAnimator != null)
        {
            leftHandAnimator = LeftArm.transform.GetComponent<Animator>();
        }
    }

    public void AnimateTakePigeon()
    {
        Taken?.Invoke();
    }

    public void AnimatePlacePigeon()
    {
        Placed?.Invoke();
    }

    public void EndOfClockAnim()
    {
        LeftArm.SetActive(false);
        _arms.isWatchingTime = false;

        leftHandAnimator.enabled = false;
    }

    public void EndOfPigeonTipLife()
    {
        Destroy(gameObject);
    }
}
