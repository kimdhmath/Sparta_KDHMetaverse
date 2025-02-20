using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPUIManager : BaseUIManager
{
    FPHomeUI fPHomeUI;
    FPGameUI fpGameUI;
    FPGameOverUI fpGameOverUI;

    protected  void Awake()
    {
        fPHomeUI = GetComponentInChildren<FPHomeUI>(true);
        fPHomeUI.Init(this);
        fpGameUI = GetComponentInChildren<FPGameUI>(true);
        fpGameUI.Init(this);
        fpGameOverUI = GetComponentInChildren<FPGameOverUI>(true);
        fpGameOverUI.Init(this);

        if (GameManager.isFPFirstStart)
        {
            ChangeState(UIState.Home);
            GameManager.isFPFirstStart = false;
        }
        else
        {
            Time.timeScale = 1;
            ChangeState(UIState.Game);
        }
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void UpdateScore(int score)
    {
        fpGameUI.FPUpdateScore(score);
    }

    public void TextBestScore(int bestScore)
    {
        fPHomeUI.FPTextBestScore(bestScore);
        fpGameOverUI.FPTextBestScore(bestScore);
    }

    public void TextScore(int score)
    {
        fpGameOverUI.FPTextScore(score);
    }

    public override void ChangeState(UIState state)
    {
        currentState = state;
        fPHomeUI.SetActive(currentState);
        fpGameUI.SetActive(currentState);
        fpGameOverUI.SetActive(currentState);
    }
}
