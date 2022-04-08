using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DamageText : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 1.3f, 0);
    private Vector3 randomizeIntensity = new Vector3(0.5f, 0, 0);
    private void Start()
    {
        transform.localPosition += offset;
        transform.localPosition += new Vector3(UnityEngine.Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
            UnityEngine.Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
            UnityEngine.Random.Range(-randomizeIntensity.z, randomizeIntensity.z));

    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
