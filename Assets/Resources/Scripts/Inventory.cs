using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory : MonoBehaviour
{
    private int slotCount = 0;
    [SerializeField] private GameObject slotPrefab;
    private float slotHeight;
    private Transform firstSlotTransform = null;
    private IconProvider[] icons= new IconProvider [6];

    private void Start()
    {
        slotHeight = slotPrefab.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AddSlot();
        }
    }

    private void AddSlot()
    {
        slotCount += 1;
        Instantiate(slotPrefab, transform.position, quaternion.identity, transform);
    }

    private void RemoveSlot()
    {
        slotCount -= 1;
        ChangeBodyHeight();
    }

    private void ChangeBodyHeight()
    {
        float height = (float)(slotHeight * slotCount + 2 * (slotHeight/ 8.33));
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, height);
    }

    private float GetSlotPosition()
    {
        return -((slotCount-1) * slotHeight)-slotHeight/1.6f;
    }
}
