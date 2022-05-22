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
    private float damage = 120f;

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
            if (hit.gameObject.layer == LayerMask.NameToLayer("Enemy") || hit.gameObject.CompareTag("Player"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance <= explosionForce)
                {
                    float result = damage * (1 / Mathf.Sqrt(distance));
                    hit.SendMessage("ApplyDamage", result, SendMessageOptions.DontRequireReceiver);
                    hit.SendMessage("SendDamage", result, SendMessageOptions.DontRequireReceiver);
                }
            }
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explodePos, explosionRadius, 3f, ForceMode.Impulse);
            }

            
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
