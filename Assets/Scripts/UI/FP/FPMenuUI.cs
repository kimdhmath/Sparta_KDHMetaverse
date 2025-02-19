using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPMenuUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private Image gameStartImage;
    [SerializeField] private Image gameOverImage;

    public override void Init(BaseUIManager uiManager)
    {
        base.Init(uiManager);
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
        GameManager.Instance.isFPFirstStart = true;
    }

    public void OnClickStartButton()
    {
        GameManager.Instance.FPStartGame();
    }

    public void OnClickExitButton()
    {
        GameManager.Instance.isFPFirstStart = true;
        GameManager.Instance.ExitMiniGame();
    }


    protected override UIState GetUIState()
    {
        if (GameManager.Instance.isFPFirstStart)
        {
            gameOverImage.enabled = false;
            gameStartImage.enabled = true;
            GameManager.Instance.isFPFirstStart = false;
            return UIState.Home;
        }
        else
        {
            gameStartImage.enabled = false;
            gameOverImage.enabled = true;
            return UIState.GameOver;
        }
    }
}
