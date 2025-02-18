using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class FPPlaneController : MonoBehaviour
{
    private FPAnimationHandler animationHandler;
    private Rigidbody2D _rigidbody;

    [SerializeField] private float flapForce = 6f;
    [SerializeField] private float forwardSpeed = 3f;
    private bool isDead = false;
    private bool isFlap = false;

    private Vector3 velocity;

    private void Awake()
    {
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<FPAnimationHandler>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        Flap();
    }

    private void Flap()
    {
        velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            return;
        }

        animationHandler.Dead();
        isDead = true;
    }

    void OnFlap(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            isFlap = true;
        }
    }
}
