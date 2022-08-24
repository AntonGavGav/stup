using System;
using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private bool isSelected = false;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite defualtSprite;
    private Vector3 defualtScale;
    private ISelectable linkedObjectSelectable;

    private void Awake()
    {
        defualtScale = transform.localScale;
    }
    

    public void AddLinkedToSlotObjectSelectable(ISelectable LinkedObjectSelectable)
    {
        linkedObjectSelectable = LinkedObjectSelectable;
    }
    public void AddSprite(Sprite sprite)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = sprite;
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void Select()
    {
        transform.localScale *= 1.1f;
        transform.GetComponent<Image>().sprite = selectedSprite;
        linkedObjectSelectable.Select();
        isSelected = true;
    }

    public void Deselect()
    {
        transform.localScale = defualtScale;
        transform.GetComponent<Image>().sprite = defualtSprite;
        linkedObjectSelectable.Deselect();
        isSelected = false;
    }

    public void DeselectObjSelectable()
    {
        linkedObjectSelectable.Deselect();
    }
}
