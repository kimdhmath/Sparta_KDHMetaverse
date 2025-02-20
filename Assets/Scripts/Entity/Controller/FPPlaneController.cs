using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class FPPlaneController : MonoBehaviour
{
    //FPPlane의 애니메이션 관리 클래스
    private FPAnimationHandler animationHandler;
    private Rigidbody2D _rigidbody; //FPPlane의 Rigidbody2D 컴포넌트

    //FPPlane의 점프 힘, 앞으로 나아가는 속도, 죽음 쿨타임, 죽음 여부, 점프 여부
    [SerializeField] private float flapForce = 6f;
    [SerializeField] private float forwardSpeed = 3f;
    [SerializeField]private float deathCooldown;
    private bool isDead = false;
    private bool isFlap = false;

    //FPPlane의 현재 속도
    private Vector3 velocity;

    private void Awake()
    {
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<FPAnimationHandler>();
        
        //미니게임 시작전 일시정지
        Time.timeScale = 0;
    }

    private void Start()
    {
        //FPPlane의 UI를 설정하고 최고점수를 업데이트
        GameManager.Instance.FPUISet();
        GameManager.Instance.FPUpdateBestScore();

    }

    private void FixedUpdate()//물리연산을 처리하는 함수
    {
        //FPPlane이 죽었을때 죽음 쿨타임을 줄이고 죽음 쿨타임이 0이 되면 게임오버
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

        //FPPlane이 살아있을때 Flap메서드 호출
        Flap();
    }

    //앞으로 이동과 점프
    private void Flap()
    {
        //FPPlane의 현재 속도를 저장하고 x축의 속도 부여
        velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)//점프가 눌렸을때 y축의 속도 부여
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        //FPPlane의 속도를 적용
        _rigidbody.velocity = velocity;

        //FPPlane의 회전을 부드럽게 처리
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        float lerpAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, angle, Time.fixedDeltaTime * 5f);
        transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
    }

    public void OnCollisionEnter2D(Collision2D collision)//충돌이 일어났을때 호출
    {
        //장애물과 죽은뒤 충돌은 제외
        if (isDead)
        {
            return;
        }

        //장애물과 충돌시 죽음
        animationHandler.Dead();
        isDead = true;
        deathCooldown = 1f;
    }

    //inputValue(스페이스바, 좌클릭) 눌렸을때 호출
    void OnFlap(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            isFlap = true;
        }
    }
}
