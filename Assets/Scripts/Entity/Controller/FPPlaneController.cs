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
    [SerializeField]private float deathCooldown;
    private bool isDead = false;
    private bool isFlap = false;

    private Vector3 velocity;

    private void Awake()
    {
        _rigidbody = transform.GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<FPAnimationHandler>();
        GameManager.Instance.FPUISet();

        Time.timeScale = 0;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
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
        float lerpAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, angle, Time.fixedDeltaTime * 5f);
        transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            return;
        }

        animationHandler.Dead();
        isDead = true;
        deathCooldown = 1f;
    }

    void OnFlap(InputValue inputValue)
    {
        if(inputValue.isPressed)
        {
            isFlap = true;
        }
    }
}
