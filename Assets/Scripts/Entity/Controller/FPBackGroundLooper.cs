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
        FPObstacle[] obstacles = GameObject.FindObjectsOfType<FPObstacle>();
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        for (int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * NUMBGCOUNT;
            collision.transform.position = pos;
            return;
        }

        FPObstacle obstacle = collision.GetComponent<FPObstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
