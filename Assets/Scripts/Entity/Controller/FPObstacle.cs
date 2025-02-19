using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPObstacle : MonoBehaviour
{
    private readonly float HIGHPOSY = 0.95f;
    private readonly float LOWPOSY = -0.95f;

    private readonly float HOLESIZEMIN = 1f;
    private readonly float HOLESIZEMAX = 3f;

    [SerializeField]private Transform topObject;
    [SerializeField]private Transform bottomObject;

    [SerializeField]private float widthPadding = 4f;

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
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
}
