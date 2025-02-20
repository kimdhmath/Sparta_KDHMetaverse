using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MetaNPCController : MonoBehaviour
{
    private bool playerIsCloser = false;
    [SerializeField] private MetaUIManager metaUIManager;

    public void Awake()
    {
        metaUIManager = FindObjectOfType<MetaUIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsCloser =true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsCloser = false;
            metaUIManager.SetTalkDeactive();
        }
    } 

    void OnNPCTalk(InputValue inputValue)
    {

        if (!playerIsCloser)
        {
            return;
        }
        if (inputValue.isPressed)
        {
            if (metaUIManager.GetTalkActive())
            {
                metaUIManager.SetTalkDeactive();
            }
            else
            {
                metaUIManager.SetTalkActive();
            }
        }
    }
}
