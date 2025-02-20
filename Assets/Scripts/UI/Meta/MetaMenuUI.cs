using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetaMenuUI : BaseUI
{
    private MetaUIManager metaUIManager;

    public void Init(MetaUIManager uiManager)
    {
        metaUIManager = uiManager;
    }

    public void OnClickeMenuButton()
    {
    }

    protected override UIState GetUIState()
    {
        return UIState.None;
    }
}