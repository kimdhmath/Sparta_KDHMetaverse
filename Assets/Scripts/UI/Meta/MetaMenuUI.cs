using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetaMenuUI : BaseUI
{
    private MetaUIManager metaUIManager;
    private MetaAnimationHandler metaAnimationHandler;
    private int index = 0;
    private const int APPEARANCELENGTH = 2;

    [SerializeField] Button changeApperanceButton;
    [SerializeField] Button menuExitButton;

    public void Init(MetaUIManager uiManager)
    {
        metaUIManager = uiManager;
        changeApperanceButton.onClick.AddListener(OnClickChangeApperanceButton);
        menuExitButton.onClick.AddListener(OnClickMenuExitButton);
        metaAnimationHandler = FindAnyObjectByType<MetaAnimationHandler>();

    }

    public void OnClickChangeApperanceButton()
    {
        if(index < APPEARANCELENGTH - 1)
        {
            index++;
            metaAnimationHandler.ChangeAppearance(index);
        }
        else
        {
            index = 0;
            metaAnimationHandler.ChangeAppearance(index);
        }
    }

    public void OnClickMenuExitButton()
    {
        metaUIManager.SetMenuDeactive();
        GameManager.isStop = false;
        Time.timeScale = 1;
    }

    protected override UIState GetUIState()
    {
        return UIState.None;
    }
}