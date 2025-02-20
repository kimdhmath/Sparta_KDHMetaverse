using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaPlayer : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] RuntimeAnimatorController[] animControllers;

    public int index1 = 0;

    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        ChangeAppearance(index1);
    }

    void ChangeAppearance(int index)
    {
        if (index >= 0 && index < animControllers.Length)
        {
            playerAnimator.runtimeAnimatorController = animControllers[index];
        }
    }
}
