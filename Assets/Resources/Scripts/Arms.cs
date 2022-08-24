using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Resources.Scripts;
using UnityEngine;

public class Arms : MonoBehaviour
{
    private AnimationEventsController armsAnimationEventsController;
    private GameObject LeftArm;
    private GameObject RightArm;
    public PigeonLogic pigeonToBeHoldLogic;
    public ITakeable ItemToBeHoldITakeable;
    private Animator leftHandAnimator;
    private Animator rightHandAnimator;
    private Inventory inventory;
    private bool isPigeonInHands = false;
    public bool isWatchingTime = false;


    private void Start()
    {
        LeftArm = transform.GetChild(0).gameObject;
        RightArm = transform.GetChild(1).gameObject;
        leftHandAnimator = LeftArm.transform.GetComponent<Animator>();
        rightHandAnimator = RightArm.transform.GetComponent<Animator>();
        armsAnimationEventsController = LeftArm.GetComponent<AnimationEventsController>();
        armsAnimationEventsController.Placed += PlacePigeon;
        armsAnimationEventsController.Taken += TakePigeon;
        inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
    }

    private void FixedUpdate()
    {
        ShowClock();
    }

    private void Update()
    {
        SlotsScrollig();
        TakeSomething();
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
    

    private void TakeSomething()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPigeonInHands && !isWatchingTime)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit) && hit.transform.GetComponent<ITakeable>() != null)
                {
                    ItemToBeHoldITakeable = hit.transform.GetComponent<ITakeable>();
                    if (hit.transform.GetComponent<ITakeable>().IsReadyToBeHold(transform))
                    {
                        ItemToBeHoldITakeable.Take();
                        if (ItemToBeHoldITakeable.returnObject().GetComponent<IconProvider>() != null)
                        {
                            Sprite sprite = ItemToBeHoldITakeable.returnObject().GetComponent<IconProvider>().GetItemSprite();
                            inventory.AddSlot(sprite, ItemToBeHoldITakeable.returnObject().GetComponent<ISelectable>());
                        }
                    }
                }
            }
            else if(Input.GetKeyDown(KeyCode.E) && isPigeonInHands && !isWatchingTime)
            {
                ItemToBeHoldITakeable.Place();
            }
    }

    private void SlotsScrollig()
    {
        if (inventory.slotCount != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (inventory.selectedSlot + 1 <= inventory.slotCount)
                {
                    inventory.selectedSlot++;
                }

                inventory.UpdateSelectSlot();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (inventory.selectedSlot - 1 >= 1)
                {
                    inventory.selectedSlot--;
                }

                inventory.UpdateSelectSlot();
            }
        }
    }
    public void AnimateHandsTake()
    {
        LeftArm.SetActive(true);
        leftHandAnimator.enabled = true;
        rightHandAnimator.SetBool("HoldPigeon", true);
        leftHandAnimator.SetBool("HoldPigeon", true);
        isPigeonInHands = true;  
    }

    public void AnimateHandsPlace()
    {
        rightHandAnimator.SetBool("HoldPigeon", false);
        leftHandAnimator.SetBool("HoldPigeon", false);
        isPigeonInHands = false;
    }
    private void TakePigeon()
    {
        ItemToBeHoldITakeable.returnObject().GetComponent<PigeonLogic>().GraficalTake();
    }

    private void PlacePigeon()
    {
        ItemToBeHoldITakeable.returnObject().GetComponent<PigeonLogic>().GraficalPlace();
        ItemToBeHoldITakeable = null;
    }

}
