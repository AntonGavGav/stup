using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    private float slotHeight;
    private List<Slot> slots = new List<Slot>();
    private int prevSelectedSlot = -1;
    
    public int slotCount { get; set; } = 0;
    public int selectedSlot { get; set; } = 1;

    private void Start()
    {
        slotHeight = slotPrefab.GetComponent<RectTransform>().sizeDelta.y;
    }
    

    public void AddSlot(Sprite sprite, ISelectable slotObjSelectable)
    {
        slotCount += 1;
        GameObject slot = Instantiate(slotPrefab, transform.position, quaternion.identity, transform);
        slots.Add(slot.GetComponent<Slot>());
        slots[slotCount-1].AddSprite(sprite);
        slots[slotCount-1].AddLinkedToSlotObjectSelectable(slotObjSelectable);
        if (slotCount == 1)
        {
            UpdateSelectSlot();
        }
        else
        {
            slots[slotCount-1].DeselectObjSelectable();
        }
    }

    
    private void RemoveSlot()
    {
        slotCount -= 1;
        //dont forget to deselect if slot selected
    }
    

    public void UpdateSelectSlot()
    {
        if (selectedSlot <= slotCount)
        {
            if (!slots[selectedSlot - 1].IsSelected())
            {
                slots[selectedSlot - 1].Select();
                if (prevSelectedSlot != -1)
                {
                    slots[prevSelectedSlot - 1].Deselect();
                }
                prevSelectedSlot = selectedSlot;
            }
        }
    }
}
