using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class FPPlaneController : MonoBehaviour
{
    //FPPlane�� �ִϸ��̼� ���� Ŭ����
    private FPAnimationHandler animationHandler;
    private Rigidbody2D _rigidbody; //FPPlane�� Rigidbody2D ������Ʈ

    //FPPlane�� ���� ��, ������ ���ư��� �ӵ�, ���� ��Ÿ��, ���� ����, ���� ����
    [SerializeField] private float flapForce = 6f;
    [SerializeField] private float forwardSpeed = 3f;
    [SerializeField]private float deathCooldown;
    private bool isDead = false;
    private bool isFlap = false;

    //FPPlane�� ���� �ӵ�
    private Vector3 velocity;

    private void Awake()
    {
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<FPAnimationHandler>();
        
        //�̴ϰ��� ������ �Ͻ�����
        Time.timeScale = 0;
    }

    private void Start()
    {
        //FPPlane�� UI�� �����ϰ� �ְ������� ������Ʈ
        GameManager.Instance.FPUISet();
        GameManager.Instance.FPUpdateBestScore();

    }

    private void FixedUpdate()//���������� ó���ϴ� �Լ�
    {
        //FPPlane�� �׾����� ���� ��Ÿ���� ���̰� ���� ��Ÿ���� 0�� �Ǹ� ���ӿ���
        if (isDead)
        {
            if(deathCooldown > 0)
            {
                deathCooldown -= Time.fixedDeltaTime;
            }
            else
            {
                GameManager.Instance.FPGameOver();
            }
            return;
        }

        //FPPlane�� ��������� Flap�޼��� ȣ��
        Flap();
    }

    //������ �̵��� ����
    private void Flap()
    {
        //FPPlane�� ���� �ӵ��� �����ϰ� x���� �ӵ� �ο�
        velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)//������ �������� y���� �ӵ� �ο�
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        //FPPlane�� �ӵ��� ����
        _rigidbody.velocity = velocity;

        //FPPlane�� ȸ���� �ε巴�� ó��
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        float lerpAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, angle, Time.fixedDeltaTime * 5f);
        transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
    }

    public void OnCollisionEnter2D(Collision2D collision)//�浹�� �Ͼ���� ȣ��
    {
        //��ֹ��� ������ �浹�� ����
        if (isDead)
        {
            return;
        }

        //��ֹ��� �浹�� ����
        animationHandler.Dead();
        isDead = true;
        deathCooldown = 1f;
    }

    //inputValue(�����̽���, ��Ŭ��) �������� ȣ��
    void OnFlap(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            isFlap = true;
        }
    }
}
