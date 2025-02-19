using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class FPHomeUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private TextMeshProUGUI bestScoreText;

    public override void Init(BaseUIManager uiManager)
    {
        base.Init(uiManager);
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        GameManager.Instance.FPStartGame();
    }

    public void OnClickExitButton()
    {
        GameManager.isFPFirstStart = true;
        GameManager.Instance.ExitMiniGame();
    }


    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public void FPTextBestScore(int bestScore)
    {
        bestScoreText.text = bestScore.ToString();
    }
}
