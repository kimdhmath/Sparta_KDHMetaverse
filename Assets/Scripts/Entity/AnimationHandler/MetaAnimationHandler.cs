using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAnimationHandler : BaseAnimationHandler
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove"); //animator isMoving파라미터의 해쉬값을 미리 계산
    private static readonly int Appearance = Animator.StringToHash("Appearance");//animator appearance파라미터의 해쉬값을 미리 계산

    protected override void Awake()
    {
        base.Awake();
    }

    public void Move(Vector2 obj)
    {
        _animator.SetBool(IsMoving, obj.magnitude > 0.5f);//animator isMoving파라미터를 obj.magnitude(속도) > 0.5f일때 true 변경
    }

    public void ChangeAppearance(int index)
    {
        _animator.SetInteger(Appearance, index);//animator appearance파라미터를 index로 변경
    }
}
