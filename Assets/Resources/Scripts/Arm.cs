using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public PlayerLogic playerLogic;
    public void EndOfClockAnim()
    {
        playerLogic.ArmSetActiveFalse();
    }
}
