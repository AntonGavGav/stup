using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class bulletLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject explosionPrefab;
    public bool wasShooted = false;
    private float explosionForce = 10f;
    private float explosionRadius = 5f;

    private void Start()
    {
        
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Vector3 explodePos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explodePos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionForce, explodePos, explosionRadius, 3f, ForceMode.Impulse);
        }
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && wasShooted)
        {
            Invoke("Explode", 1.5f);
        }
    }
}
