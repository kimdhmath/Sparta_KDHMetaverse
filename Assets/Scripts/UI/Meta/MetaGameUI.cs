using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetaGameUI : BaseUI
{
    private MetaUIManager metaUIManager;

    [SerializeField] private Button menuButton;

    public void Init(MetaUIManager uiManager)
    {
        metaUIManager = uiManager;
        menuButton.onClick.AddListener(OnClickeMenuButton);
    }

    public void OnClickeMenuButton()
    {
        metaUIManager.SetMenuActive();
        GameManager.isStop = true;
        Time.timeScale = 0;
    }



    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
