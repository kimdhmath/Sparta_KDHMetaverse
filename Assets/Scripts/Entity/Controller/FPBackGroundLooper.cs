using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPBackGroundLooper : MonoBehaviour
{
    private readonly int NUMBGCOUNT = 5;

    private int obstacleCount = 0;
    private Vector3 obstacleLastPosition = Vector3.zero;

    void Start()
    {
        FPObstacle[] obstacles = GameObject.FindObjectsOfType<FPObstacle>();//��ֹ����� ã�Ƽ� �迭�� ����
        obstacleLastPosition = obstacles[0].transform.position;//ù��° ��ֹ��� ��ġ�� ����
        obstacleCount = obstacles.Length;//��ֹ��� ������ ����

        //�����ֹ����� ��ġ�� x�ุŭ �����ϰ� y�����δ� �����ϰ� ����
        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)//�浹�� �Ͼ������ ȣ��
    {
        //����̹����� �浹�ϸ� ��� �̹����� �ʺ�� �־��� ���� ��ŭ x������ �̵�
        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * NUMBGCOUNT;
            collision.transform.position = pos;
            return;
        }

        //��ֹ��� �浹�ϸ� ��ֹ��� ��ġ�� x�ุŭ �����ϰ� y�����δ� �����ϰ� ����
        FPObstacle obstacle = collision.GetComponent<FPObstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition);
        }
    }
}
