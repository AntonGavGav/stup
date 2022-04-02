using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonAtack : MonoBehaviour
{
    public PlayerLogic playerLogic;
    
    public void Atack(){
        playerLogic.SendDamage(10);
    }
}
