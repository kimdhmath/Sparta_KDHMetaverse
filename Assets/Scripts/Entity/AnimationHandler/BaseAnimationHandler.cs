using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnimationHandler : MonoBehaviour
{
    //AnimationHandler
    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
