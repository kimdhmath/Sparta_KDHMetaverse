using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPGameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public override void Init(BaseUIManager uiManager)
    {
        base.Init(uiManager);
    }

    public void FPUpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
