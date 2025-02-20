using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MetaNPCController : MonoBehaviour
{
    //�÷��̾ NPC�� ������ �ִ��� Ȯ���ϴ� ����
    private bool playerIsCloser = false;
    [SerializeField] private MetaUIManager metaUIManager;//npc�� ��ȭâ�� ���� ���� ����

    public void Awake()
    {
        metaUIManager = FindObjectOfType<MetaUIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)//�÷��̾ NPC�� ������ ������ ��ȭâ�� ���� �ֱ� ���� �Լ�
    {
        if (collision.CompareTag("Player"))
        {
            playerIsCloser =true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)//�÷��̾ NPC�� ���� ������ ������ ��ȭâ�� �ݱ����� �Լ�
    {
        if (collision.CompareTag("Player"))
        {
            playerIsCloser = false;
            metaUIManager.SetTalkDeactive();//��ȭâ �ݱ�
        }
    }

    //�÷��̾ NPC�� ������ ������ inputvalue(eŰ)�� ������ ��ȭâ ����
    void OnNPCTalk(InputValue inputValue)
    {

        if (!playerIsCloser || GameManager.isStop)//�÷��̾ NPC�� ������ ���� �ʰų� ������ ���������� ��ȭâ�� ���� ����
        {
            return;
        }

        if (inputValue.isPressed)//eŰ�� ������ ��ȭâ�� ���ų� ����
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
