using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPAnimationHandler : BaseAnimationHandler
{
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    protected Animator animator;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Dead()
    {
        _animator.SetBool(IsDead,true);
    }
}
