using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPAnimationHandler : BaseAnimationHandler
{
    //Animatior���� isDie�Ķ������ �ؽ����� �̸� ���
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    protected Animator animator;

    protected override void Awake()
    {
        base.Awake();
    }


    //isDie�Ķ���� true�� ����
    public void Dead()
    {
        _animator.SetBool(IsDie,true);
    }
}
