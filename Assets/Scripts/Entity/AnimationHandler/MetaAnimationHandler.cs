using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaAnimationHandler : BaseAnimationHandler
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove"); //animator isMoving�Ķ������ �ؽ����� �̸� ���
    private static readonly int Appearance = Animator.StringToHash("Appearance");//animator appearance�Ķ������ �ؽ����� �̸� ���

    protected override void Awake()
    {
        base.Awake();
    }

    public void Move(Vector2 obj)
    {
        _animator.SetBool(IsMoving, obj.magnitude > 0.5f);//animator isMoving�Ķ���͸� obj.magnitude(�ӵ�) > 0.5f�϶� true ����
    }

    public void ChangeAppearance(int index)
    {
        _animator.SetInteger(Appearance, index);//animator appearance�Ķ���͸� index�� ����
    }
}
