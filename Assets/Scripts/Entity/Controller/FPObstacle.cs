using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPObstacle : MonoBehaviour
{
    //장애물의 y축 위치 범위
    private readonly float HIGHPOSY = 0.95f;
    private readonly float LOWPOSY = -0.95f;

    //장애물의 구멍 크기 범위
    private readonly float HOLESIZEMIN = 1f;
    private readonly float HOLESIZEMAX = 3f;

    //top장애물과 bottom장애물의 위치(position), 회전(rotation), 크기(scale) 정보 컴포넌트
    [SerializeField]private Transform topObject;
    [SerializeField]private Transform bottomObject;

    //장애물 사이의 간격
    [SerializeField]private float widthPadding = 4f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    //마지막 위치를 기준으로 widthPadding만큼 x축 이동후 y축은 범위 안에서 랜덤하게 배치
    public Vector3 SetRandomPlace(Vector3 lastPosition)
    {
        float holeSize = Random.Range(HOLESIZEMIN, HOLESIZEMAX);
        float halfHoleSize = holeSize / 2f;
        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(LOWPOSY, HIGHPOSY);

        transform.position = placePosition;

        return placePosition;
    }

    //플레이어가 장애물을 벗어날때마다 점수 추가
    private void OnTriggerExit2D(Collider2D other)
    {
        FPPlaneController player = other.GetComponent<FPPlaneController>();
        if (player != null)
        {
            gameManager.FPAddScore();
        }
    }
}
