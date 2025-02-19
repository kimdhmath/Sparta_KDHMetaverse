using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPGameUI : BaseUI
{

    public override void Init(BaseUIManager uiManager)
    {
        base.Init(uiManager);
    }


    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}
