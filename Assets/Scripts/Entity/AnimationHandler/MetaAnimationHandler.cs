using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAnimationHandler : BaseAnimationHandler
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");

    protected Animator animator;

    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > 0.5f);
    }
}
