using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TSUIManager : BaseUIManager
{
    TSHomeUI tsHomeUI = null;
    TSGameUI tsGameUI = null;
    TSGameOverUI tsGameOverUI = null;

    protected  void Awake()
    {
        tsHomeUI = GetComponentInChildren<TSHomeUI>(true);
        tsHomeUI.Init(this);
        tsGameUI = GetComponentInChildren<TSGameUI>(true);
        tsGameUI.Init(this);
        tsGameOverUI = GetComponentInChildren<TSGameOverUI>(true);
        tsGameOverUI.Init(this);

        if(GameManager.isTSFirstStart)
        {
            ChangeState(UIState.Home);
            GameManager.isTSFirstStart = false;
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
        tsGameUI.TSUpdateScore(score);
    }

    public void TextBestScore(int bestScore)
    {
        tsHomeUI.TSTextBestScore(bestScore);
        tsGameOverUI.TSTextBestScore(bestScore);
    }

    public void TextScore(int score)
    {
        tsGameOverUI.TSTextScore(score);
    }

    public override void ChangeState(UIState state)
    {
        currentState = state;
        tsHomeUI.SetActive(currentState);
        tsGameUI.SetActive(currentState);
        tsGameOverUI.SetActive(currentState);
    }
}
