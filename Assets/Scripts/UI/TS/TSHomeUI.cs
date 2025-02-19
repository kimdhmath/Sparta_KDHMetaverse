using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TSHomeUI : BaseUI
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
        GameManager.Instance.TSStartGame();
    }

    public void OnClickExitButton()
    {
        GameManager.isTSFirstStart = true;
        GameManager.Instance.ExitMiniGame();
    }


    protected override UIState GetUIState()
    {
        return UIState.Home;
    }

    public void TSTextBestScore(int bestScore)
    {
        bestScoreText.text = bestScore.ToString();
    }
}
