using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPUIManager : BaseUIManager
{
    FPMenuUI fpMenuUI;
    FPGameUI fpGameUI;
    FPGameOverUI fpGameOverUI;

    protected override void Awake()
    {
        fpMenuUI = GetComponentInChildren<FPMenuUI>(true);
        fpMenuUI.Init(this);
        fpGameUI = GetComponentInChildren<FPGameUI>(true);
        fpGameUI.Init(this);

        ChangeState(UIState.Home);
    }

    public override void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public override void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public override void ChangeState(UIState state)
    {
        currentState = state;
        fpMenuUI.SetActive(currentState);
        fpGameUI.SetActive(currentState);
    }
}
