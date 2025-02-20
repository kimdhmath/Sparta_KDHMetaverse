using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPAnimationHandler : BaseAnimationHandler
{
    //Animatior에서 isDie파라미터의 해쉬값을 미리 계산
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    protected Animator animator;

    protected override void Awake()
    {
        base.Awake();
    }


    //isDie파라미터 true로 변경
    public void Dead()
    {
        _animator.SetBool(IsDie,true);
    }
}
