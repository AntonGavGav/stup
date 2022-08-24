using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{

    public delegate void AnimationEndHandler();
    public event AnimationEndHandler AnimationEnded;

    public void EndAnimation()
    {
        AnimationEnded?.Invoke();
    }
}
