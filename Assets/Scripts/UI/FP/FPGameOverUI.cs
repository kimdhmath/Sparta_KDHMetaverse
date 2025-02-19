using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FPGameOverUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private TextMeshProUGUI scoreText;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickExitButton()
    {
        GameManager.isFPFirstStart = true;
        GameManager.Instance.ExitMiniGame();
    }


    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }

    public void FPTextBestScore(int bestScore)
    {
        bestScoreText.text = bestScore.ToString();
    }

    public void FPTextScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
