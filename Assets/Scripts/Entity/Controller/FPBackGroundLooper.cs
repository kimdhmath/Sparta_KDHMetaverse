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
        FPObstacle[] obstacles = GameObject.FindObjectsOfType<FPObstacle>();//장애물들을 찾아서 배열에 저장
        obstacleLastPosition = obstacles[0].transform.position;//첫번째 장애물의 위치를 저장
        obstacleCount = obstacles.Length;//장애물의 갯수를 저장

        //모든장애물들의 위치를 x축만큼 일정하게 y축으로는 랜덤하게 설정
        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)//충돌이 일어날때마다 호출
    {
        //배경이미지가 충돌하면 배경 이미지의 너비와 주어진 개수 만큼 x축으로 이동
        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * NUMBGCOUNT;
            collision.transform.position = pos;
            return;
        }

        //장애물이 충돌하면 장애물의 위치를 x축만큼 일정하게 y축으로는 랜덤하게 설정
        FPObstacle obstacle = collision.GetComponent<FPObstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition);
        }
    }
}
