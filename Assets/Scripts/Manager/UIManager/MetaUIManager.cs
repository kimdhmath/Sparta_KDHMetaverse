using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaUIManager : BaseUIManager
{
    private MetaGameUI metaGameUI;
    private MetaNPCTalkUI metaNPCTalkUI;
    private MetaMenuUI metaMenuUI;

    protected void Awake()
    {
        metaGameUI = GetComponentInChildren<MetaGameUI>(true);
        metaGameUI.Init(this);
        metaNPCTalkUI = GetComponentInChildren<MetaNPCTalkUI>(true);
        metaNPCTalkUI.Init(this);
        metaMenuUI = GetComponentInChildren<MetaMenuUI>(true);
        metaMenuUI.Init(this);

        ChangeState(UIState.Game);
    }


    public void SetTalkActive()
    {
        metaNPCTalkUI.ZeroText();
        metaNPCTalkUI.gameObject.SetActive(true);
        metaNPCTalkUI.NextLine();
    }

    public bool GetTalkActive()
    {
        return metaNPCTalkUI.gameObject.activeSelf;
    }

    public void SetTalkDeactive()
    {
        metaNPCTalkUI.gameObject.SetActive(false);
    }

    public void SetMenuActive()
    {
        metaMenuUI.gameObject.SetActive(true);
    }

    public void SetMenuDeactive()
    {
        metaMenuUI.gameObject.SetActive(false);
    }

    public override void ChangeState(UIState state)
    {
        currentState = state;
        metaGameUI.SetActive(currentState);
        metaNPCTalkUI.SetActive(currentState);
        metaMenuUI.SetActive(currentState);
    }
}
