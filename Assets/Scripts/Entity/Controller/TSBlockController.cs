using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TSBlockController : MonoBehaviour
{
    //블록의 크기, 블록 이동 범위, 블록 쌓는 속도, 블록 이동 속도, 오차범위
    private const float BoundSize = 3.5f;
    private const float MovingBoundSize = 3f;
    private const float StackMovingSpeed = 5.0f;
    private const float BlockMovingSpeed = 3.5f;
    private const float ErrorMargin = 0.1f;

    [SerializeField]private GameObject originBlock = null;//블록의 원본

    //블록의 이전 위치, 원하는 위치, 블록의 크기
    private Vector3 prevBlockPosition;
    private Vector3 desiredPosition;
    private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);

    //마지막 블록, 블록 이동, 블록의 보조 위치
    Transform lastBlock = null;
    float blockTransition = 0f;
    float secondaryPosition = 0f;

    //스택된 개수
    int stackCount = -1;

    //색상 이전, 이후
    public Color prevColor;
    public Color nextColor;

    //블록이 x축으로 이동 확인
    bool isMovingX = true;

    //게임오버 확인
    private bool isGameOver = true;

    void Start()
    {
        //베스트 스코어 설정
        GameManager.Instance.TSUISet();
        GameManager.Instance.TSUpdateBestScore();

        TSStartGame();
    }

    //게임 시작
    public void TSStartGame()
    {
        //원본 블록이 없을때
        if (originBlock == null)
        {
            Debug.LogError("Originblock is null");
            return;
        }

        //게임 시작
        isGameOver = false;

        //색상 랜덤으로 설정
        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        //이전 블록 위치 설정
        prevBlockPosition = Vector3.down;

        //블록 생성
        Spawn_Block();
        Spawn_Block();
    }

    void Update()
    {
        //게임오버일때
        if (isGameOver)
        {
            return;
        }

        //블록 이동
        MoveBlaock();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);//블록 쌓기
    }

    //블록 생성
    bool Spawn_Block()
    {
        if (lastBlock != null)//마지막 블록이 있을때
        {
            prevBlockPosition = lastBlock.localPosition;//이전 블록 위치 설정
        }

        //새로운 블록 생성
        GameObject newBlock = null;
        Transform newTrans = null;

        newBlock = Instantiate(originBlock);

        //새로운 블록이 없을때
        if (newBlock == null)
        {
            Debug.LogError("Failed to instantiate block");
            return false;
        }

        //블록 색상 변경
        ColorChange(newBlock);

        //새로운 블록의 부모 설정
        newTrans = newBlock.transform;
        newTrans.parent = this.transform;
        newTrans.localPosition = prevBlockPosition + Vector3.up;
        newTrans.localRotation = Quaternion.identity;
        newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

        //스택된 개수 증가
        stackCount++;

        //원하는 위치 설정
        desiredPosition = Vector3.down * stackCount;
        blockTransition = 0f;

        //마지막 블록 설정
        lastBlock = newTrans;

        //X축으로 이동인지 역전
        isMovingX = !isMovingX;

        //스코어 업데이트
        GameManager.Instance.TSUpadateScore(stackCount);

        return true;
    }

    //랜덤 색상 반환
    Color GetRandomColor()
    {
        float r = Random.Range(100f, 250f) / 255f
            , g = Random.Range(100f, 250f) / 255f
            , b = Random.Range(100f, 250f) / 255f;

        return new Color(r, g, b);
    }

    //색상 변경
    void ColorChange(GameObject go)
    {
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        Renderer rn = go.GetComponent<Renderer>();

        if (rn == null)
        {
            Debug.LogError("Renderer is null");
            return;
        }

        rn.material.color = applyColor;//블록 색상 변경
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);//카메라 배경색 변경

        if (applyColor.Equals(nextColor))
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
    }

    void MoveBlaock()//블록 이동
    {
        //블록 이동 속도 설정
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        //블록 이동 범위 설정
        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

        if (isMovingX)//X축으로 이동
        {
            lastBlock.localPosition = new Vector3(movePosition * MovingBoundSize, stackCount, secondaryPosition);
        }
        else//Z축으로 이동
        {
            lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundSize);
        }
    }

    bool PlaceBlock()//블록 쌓기
    {
        Vector3 lastPosition = lastBlock.localPosition;

        if (isMovingX)//X축으로 이동
        {
            float deltaX = prevBlockPosition.x - lastPosition.x;//블록간의 차이
            bool isNegativeNum = (deltaX < 0) ? true : false;//음수인지 확인

            //절대값으로 변경
            deltaX = Mathf.Abs(deltaX);
            if (deltaX > ErrorMargin)//오차범위보다 클때
            {
                stackBounds.x -= deltaX;//차이만큼 스캑 크기 줄이기
                if (stackBounds.x <= 0)//스캑 크기가 0보다 작을때 실패
                {
                    return false;
                }
                float middle = (prevBlockPosition.x + lastPosition.x) / 2f;//중간값 설정
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);//스캑 크기 설정

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.x = middle;
                lastBlock.localPosition = tempPosition;

                float rubbleHalfScale = deltaX / 2f;
                //차이만큼 러블 생성
                CreateRubble(
                    new Vector3(
                        isNegativeNum
                        ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                        : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale
                        , lastPosition.y
                        , lastPosition.z
                        ),
                    new Vector3(deltaX, 1, stackBounds.y)
                );
            }
            else//오차범위보다 작을때
            {
                //오차 수정하여 블록 쌓기
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }
        else//Z축으로 이동인지 확인 위과정 z축으로 반복
        {
            float deltaZ = prevBlockPosition.z - lastPosition.z;
            bool isNegativeNum = (deltaZ < 0) ? true : false;

            deltaZ = Mathf.Abs(deltaZ);
            if (deltaZ > ErrorMargin)
            {
                stackBounds.y -= deltaZ;
                if (stackBounds.y <= 0)
                {
                    return false;
                }
                float middle = (prevBlockPosition.z + lastPosition.z) / 2f;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.z = middle;
                lastBlock.localPosition = tempPosition;

                float rubbleHalfScale = deltaZ / 2f;
                CreateRubble(
                    new Vector3(
                        lastPosition.x
                        , lastPosition.y
                        , isNegativeNum
                        ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                        : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale
                        ),
                    new Vector3(stackBounds.x, 1, deltaZ)
                );
            }
            else
            {
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }
        secondaryPosition = (isMovingX) ? lastBlock.localPosition.x : lastBlock.localPosition.z;

        return true;
    }

    //러블 생성(차이난 만큼의 나머지)
    void CreateRubble(Vector3 pos, Vector3 scale)
    {
        //러블 생성
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;

        //러블 위치, 크기, 회전 설정
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.transform.rotation = Quaternion.identity;

        //러블 리지드바디 추가
        go.AddComponent<Rigidbody>();
        go.name = "Rubble";//러블 이름 설정
    }

    void GameOverEffect()//게임오버 이펙트
    {
        if(isGameOver)//이미 게임오버일때 리턴
        {
            return;
        }

        int childCount = this.transform.childCount;

        for (int i = 1; i < 20; i++)
        {
            if (childCount < i)
            {
                break;
            }

            GameObject go = transform.GetChild(childCount - i).gameObject;

            if (go.name.Equals("Rubble"))
            {
                continue;
            }

            Rigidbody rigid = go.AddComponent<Rigidbody>();

            //러블에 랜덤한 힘을 가함
            rigid.AddForce(
                (Vector3.up * Random.Range(0f, 10f) + Vector3.right * (Random.Range(0f, 10f) - 5f)) * 100f
                );
        }
    }

    public void Restart()//재시작
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        isGameOver = false;

        lastBlock = null;
        desiredPosition = Vector3.zero;
        stackBounds = new Vector3(BoundSize, BoundSize);

        stackCount = -1;
        isMovingX = true;
        blockTransition = 0f;
        secondaryPosition = 0f;

        prevBlockPosition = Vector3.down;

        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        Spawn_Block();
        Spawn_Block();
    }

    //스페이스바나 좌클릭시 블록 쌓기
    private void OnStack(InputValue inputValue)
    {
        if (EventSystem.current.IsPointerOverGameObject())//UI위에 마우스가 있을때 블록 쌓기를 하지 않음
        {
            return;
        }
        if (inputValue.isPressed)//스페이스바나 좌클릭시 블록 쌓기
        {
            if (PlaceBlock())//블록 쌓기가 가능하면
            {
                Spawn_Block();//블록 생성
            }
            else//블록 쌓기가 불가능하면
            {
                GameOverEffect();//게임오버 이펙트
                isGameOver = true;
                GameManager.Instance.TSGameOver();//게임오버
            }
        }
    }
}