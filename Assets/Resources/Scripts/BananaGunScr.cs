using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaGunScr : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    private Animator animator;
    private Transform bulletTransform;
    public enum State
    {
        Shooting,
        Reloading
    }

    public State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Shooting;
        animator = GetComponent<Animator>();
        bulletTransform = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Shooting:
                if (Input.GetMouseButtonDown(0))
                {
                    Shooting();
                }
                break;
            case  State.Reloading:
                Invoke("CreateBullet", 2.3f);
                state = State.Shooting;
                break;
        }
    }
    void Shooting()
    {
        if (transform.childCount > 2)
        {
            GameObject bullet = transform.GetChild(2).gameObject;
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bullet.GetComponent<bulletLogic>().wasShooted = true;
            bullet.transform.parent = null;
            bulletRb.useGravity = true;
            bulletRb.isKinematic = false;
            bulletRb.AddForce(transform.forward * UnityEngine.Random.Range(10f, 15f), ForceMode.Impulse);
            //animation to reload a bananagun:
            animator.SetTrigger("Reload");
            state = State.Reloading;
        }
        
    }

    void CreateBullet()
    {
        Instantiate(bulletPref, bulletTransform.position,  bulletTransform.rotation).transform.parent = bulletTransform.parent;
    }
}
