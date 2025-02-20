using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetaNPCTalkUI : BaseUI
{
    private MetaUIManager metaUIManager;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] dialogue;
    [SerializeField] private float wordSpeed = 0.1f;
    private int index = 0;

    [SerializeField] private Button nextButton;

    public void Init(MetaUIManager uiManager)
    {
        metaUIManager = uiManager;
        nextButton.onClick.AddListener(OnClickeNextButton);
    }

    public void Update()
    {
        if (dialogueText.text == dialogue[index - 1])
        {
            nextButton.gameObject.SetActive(true);
        }
    }
    public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        nextButton.gameObject.SetActive(false);
        if (index <= dialogue.Length - 1)
        {
            dialogueText.text = "";
            StartCoroutine(Typing());
            index++;
        }
        else
        {
            ZeroText();
        }
    }

    public void OnClickeNextButton()
    {
        if(index == dialogue.Length)
        {
            metaUIManager.SetTalkDeactive();
        }
        else
        {
            NextLine();
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.None;
    }
}
