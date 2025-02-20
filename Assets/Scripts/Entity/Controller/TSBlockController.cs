using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TSBlockController : MonoBehaviour
{
    //����� ũ��, ��� �̵� ����, ��� �״� �ӵ�, ��� �̵� �ӵ�, ��������
    private const float BoundSize = 3.5f;
    private const float MovingBoundSize = 3f;
    private const float StackMovingSpeed = 5.0f;
    private const float BlockMovingSpeed = 3.5f;
    private const float ErrorMargin = 0.1f;

    [SerializeField]private GameObject originBlock = null;//����� ����

    //����� ���� ��ġ, ���ϴ� ��ġ, ����� ũ��
    private Vector3 prevBlockPosition;
    private Vector3 desiredPosition;
    private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);

    //������ ���, ��� �̵�, ����� ���� ��ġ
    Transform lastBlock = null;
    float blockTransition = 0f;
    float secondaryPosition = 0f;

    //���õ� ����
    int stackCount = -1;

    //���� ����, ����
    public Color prevColor;
    public Color nextColor;

    //����� x������ �̵� Ȯ��
    bool isMovingX = true;

    //���ӿ��� Ȯ��
    private bool isGameOver = true;

    void Start()
    {
        //����Ʈ ���ھ� ����
        GameManager.Instance.TSUISet();
        GameManager.Instance.TSUpdateBestScore();

        TSStartGame();
    }

    //���� ����
    public void TSStartGame()
    {
        //���� ����� ������
        if (originBlock == null)
        {
            Debug.LogError("Originblock is null");
            return;
        }

        //���� ����
        isGameOver = false;

        //���� �������� ����
        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        //���� ��� ��ġ ����
        prevBlockPosition = Vector3.down;

        //��� ����
        Spawn_Block();
        Spawn_Block();
    }

    void Update()
    {
        //���ӿ����϶�
        if (isGameOver)
        {
            return;
        }

        //��� �̵�
        MoveBlaock();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);//��� �ױ�
    }

    //��� ����
    bool Spawn_Block()
    {
        if (lastBlock != null)//������ ����� ������
        {
            prevBlockPosition = lastBlock.localPosition;//���� ��� ��ġ ����
        }

        //���ο� ��� ����
        GameObject newBlock = null;
        Transform newTrans = null;

        newBlock = Instantiate(originBlock);

        //���ο� ����� ������
        if (newBlock == null)
        {
            Debug.LogError("Failed to instantiate block");
            return false;
        }

        //��� ���� ����
        ColorChange(newBlock);

        //���ο� ����� �θ� ����
        newTrans = newBlock.transform;
        newTrans.parent = this.transform;
        newTrans.localPosition = prevBlockPosition + Vector3.up;
        newTrans.localRotation = Quaternion.identity;
        newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

        //���õ� ���� ����
        stackCount++;

        //���ϴ� ��ġ ����
        desiredPosition = Vector3.down * stackCount;
        blockTransition = 0f;

        //������ ��� ����
        lastBlock = newTrans;

        //X������ �̵����� ����
        isMovingX = !isMovingX;

        //���ھ� ������Ʈ
        GameManager.Instance.TSUpadateScore(stackCount);

        return true;
    }

    //���� ���� ��ȯ
    Color GetRandomColor()
    {
        float r = Random.Range(100f, 250f) / 255f
            , g = Random.Range(100f, 250f) / 255f
            , b = Random.Range(100f, 250f) / 255f;

        return new Color(r, g, b);
    }

    //���� ����
    void ColorChange(GameObject go)
    {
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        Renderer rn = go.GetComponent<Renderer>();

        if (rn == null)
        {
            Debug.LogError("Renderer is null");
            return;
        }

        rn.material.color = applyColor;//��� ���� ����
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);//ī�޶� ���� ����

        if (applyColor.Equals(nextColor))
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
    }

    void MoveBlaock()//��� �̵�
    {
        //��� �̵� �ӵ� ����
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        //��� �̵� ���� ����
        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

        if (isMovingX)//X������ �̵�
        {
            lastBlock.localPosition = new Vector3(movePosition * MovingBoundSize, stackCount, secondaryPosition);
        }
        else//Z������ �̵�
        {
            lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundSize);
        }
    }

    bool PlaceBlock()//��� �ױ�
    {
        Vector3 lastPosition = lastBlock.localPosition;

        if (isMovingX)//X������ �̵�
        {
            float deltaX = prevBlockPosition.x - lastPosition.x;//��ϰ��� ����
            bool isNegativeNum = (deltaX < 0) ? true : false;//�������� Ȯ��

            //���밪���� ����
            deltaX = Mathf.Abs(deltaX);
            if (deltaX > ErrorMargin)//������������ Ŭ��
            {
                stackBounds.x -= deltaX;//���̸�ŭ ��Ĵ ũ�� ���̱�
                if (stackBounds.x <= 0)//��Ĵ ũ�Ⱑ 0���� ������ ����
                {
                    return false;
                }
                float middle = (prevBlockPosition.x + lastPosition.x) / 2f;//�߰��� ����
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);//��Ĵ ũ�� ����

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.x = middle;
                lastBlock.localPosition = tempPosition;

                float rubbleHalfScale = deltaX / 2f;
                //���̸�ŭ ���� ����
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
            else//������������ ������
            {
                //���� �����Ͽ� ��� �ױ�
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }
        else//Z������ �̵����� Ȯ�� ������ z������ �ݺ�
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

    //���� ����(���̳� ��ŭ�� ������)
    void CreateRubble(Vector3 pos, Vector3 scale)
    {
        //���� ����
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;

        //���� ��ġ, ũ��, ȸ�� ����
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.transform.rotation = Quaternion.identity;

        //���� ������ٵ� �߰�
        go.AddComponent<Rigidbody>();
        go.name = "Rubble";//���� �̸� ����
    }

    void GameOverEffect()//���ӿ��� ����Ʈ
    {
        if(isGameOver)//�̹� ���ӿ����϶� ����
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

            //���� ������ ���� ����
            rigid.AddForce(
                (Vector3.up * Random.Range(0f, 10f) + Vector3.right * (Random.Range(0f, 10f) - 5f)) * 100f
                );
        }
    }

    public void Restart()//�����
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

    //�����̽��ٳ� ��Ŭ���� ��� �ױ�
    private void OnStack(InputValue inputValue)
    {
        if (EventSystem.current.IsPointerOverGameObject())//UI���� ���콺�� ������ ��� �ױ⸦ ���� ����
        {
            return;
        }
        if (inputValue.isPressed)//�����̽��ٳ� ��Ŭ���� ��� �ױ�
        {
            if (PlaceBlock())//��� �ױⰡ �����ϸ�
            {
                Spawn_Block();//��� ����
            }
            else//��� �ױⰡ �Ұ����ϸ�
            {
                GameOverEffect();//���ӿ��� ����Ʈ
                isGameOver = true;
                GameManager.Instance.TSGameOver();//���ӿ���
            }
        }
    }
}