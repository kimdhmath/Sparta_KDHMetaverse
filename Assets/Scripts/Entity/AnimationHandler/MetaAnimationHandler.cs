using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAnimationHandler : BaseAnimationHandler
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int Appearance = Animator.StringToHash("Appearance");

    protected override void Awake()
    {
        base.Awake();
    }

    public void Move(Vector2 obj)
    {
        _animator.SetBool(IsMoving, obj.magnitude > 0.5f);
    }

    public void ChangeAppearance(int index)
    {
        _animator.SetInteger(Appearance, index);
    }
}
