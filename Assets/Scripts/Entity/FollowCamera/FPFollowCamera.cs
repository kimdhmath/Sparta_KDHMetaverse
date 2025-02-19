using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FPFollowCamera : BaseFollowCamera
{
    private float offsetX;

    protected virtual void Start()
    {
        if (target == null)
            return;

        offsetX = transform.position.x - target.position.x;
    }

    protected void LateUpdate()
    {
        if (target == null)
            return;
        Vector3 pos = transform.position;
        pos.x = target.position.x + offsetX;
        transform.position = pos;
    }
}
