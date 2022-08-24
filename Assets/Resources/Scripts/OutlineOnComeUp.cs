using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineOnComeUp : MonoBehaviour
{
    private Outline outline;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (GetComponent<Outline>() != null)
        {
            outline = GetComponent<Outline>();
        }
        else
        {
            outline = transform.GetChild(0).GetComponent<Outline>();
        }
    }
    private void OnMouseOver()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) < 3f)
        {
            outline.enabled = true;
        }
        else
        {
            outline.enabled = false;
        }
    }

    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}
