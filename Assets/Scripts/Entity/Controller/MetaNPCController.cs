using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MetaNPCController : MonoBehaviour
{
    //플레이어가 NPC와 가까이 있는지 확인하는 변수
    private bool playerIsCloser = false;
    [SerializeField] private MetaUIManager metaUIManager;//npc의 대화창을 열기 위한 변수

    public void Awake()
    {
        metaUIManager = FindObjectOfType<MetaUIManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)//플레이어가 NPC와 가까이 있을때 대화창을 열수 있기 위한 함수
    {
        if (collision.CompareTag("Player"))
        {
            playerIsCloser =true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)//플레이어가 NPC의 범위 밖으로 나가면 대화창을 닫기위한 함수
    {
        if (collision.CompareTag("Player"))
        {
            playerIsCloser = false;
            metaUIManager.SetTalkDeactive();//대화창 닫기
        }
    }

    //플레이어가 NPC와 가까이 있을때 inputvalue(e키)를 눌리면 대화창 열기
    void OnNPCTalk(InputValue inputValue)
    {

        if (!playerIsCloser || GameManager.isStop)//플레이어가 NPC와 가까이 있지 않거나 게임이 멈춰있을때 대화창을 열지 않음
        {
            return;
        }

        if (inputValue.isPressed)//e키를 누르면 대화창을 열거나 닫음
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
