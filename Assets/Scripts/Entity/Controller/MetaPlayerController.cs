using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MetaPlayerController : MonoBehaviour
{
    //플레이어의 이동속도
    [SerializeField] private float speed = 5f;
    //플레이어의 캐릭터 렌더러
    [SerializeField] private SpriteRenderer characterRenderer;

    private Rigidbody2D _rigidbody;
    private MetaAnimationHandler metaAnimationHandler;

    //플레이어의 이동방향
    protected Vector2 movementDirection = Vector2.zero;

    public Vector2 MovementDirection { get { return movementDirection; } }

    //플레이어의 바라보는 방향
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        metaAnimationHandler = GetComponent<MetaAnimationHandler>();
    }

    private void Update()//플레이어의 이동방향과 바라보는 방향을 설정
    {
        Rotate(lookDirection);
    }

    private void FixedUpdate()
    {
        Movement(movementDirection);
    }

    private void Movement(Vector2 direction)//플레이어의 이동
    {
        direction = direction * speed;

        _rigidbody.velocity = direction;
        metaAnimationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)//플레이어의 바라보는 방향 설정
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;
    }

    //wasd키를 누르면 플레이어의 이동방향과 바라보는 방향을 설정
    void OnMove(InputValue inputValue)
    {
        if (GameManager.isStop)
        {
            return;
        }
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;

        lookDirection = inputValue.Get<Vector2>();
        lookDirection = lookDirection.normalized;
    }
}
