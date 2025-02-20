using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MetaPlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private SpriteRenderer characterRenderer;

    private Rigidbody2D _rigidbody;
    private MetaAnimationHandler metaAnimationHandler;
    private Camera mainCamera;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        metaAnimationHandler = GetComponent<MetaAnimationHandler>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Rotate(lookDirection);
    }

    private void FixedUpdate()
    {
        Movement(movementDirection);
    }

    private void Movement(Vector2 direction)
    {
        direction = direction * speed;

        _rigidbody.velocity = direction;
        metaAnimationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;
    }

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
