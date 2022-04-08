using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonAtack : MonoBehaviour
{
    public PlayerLogic playerLogic;
    
    public void Atack(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.SendMessage("SendDamage", 10, SendMessageOptions.DontRequireReceiver);
        }
    }
}
