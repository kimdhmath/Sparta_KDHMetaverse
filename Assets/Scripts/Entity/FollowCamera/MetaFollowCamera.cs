using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaFollowCamera : BaseFollowCamera
{
    private readonly float TOP_BOUNDARY = 4.2f;
    private readonly float BOTTOM_BOUNDARY = -6f;
    private readonly float LEFT_BOUNDARY = -5.2f;
    private readonly float RIGHT_BOUNDARY = 6.2f;

    protected void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 pos = transform.position;
        pos.x = target.position.x;
        pos.y = target.position.y;
        if(pos.x <= LEFT_BOUNDARY)
        {
            pos.x = LEFT_BOUNDARY;
        }
        else if (pos.x >= RIGHT_BOUNDARY)
        {
            pos.x = RIGHT_BOUNDARY;
        }
        if (pos.y <= BOTTOM_BOUNDARY)
        {
            pos.y = BOTTOM_BOUNDARY;
        }
        else if (pos.y >= TOP_BOUNDARY)
        {
            pos.y = TOP_BOUNDARY;
        }
        transform.position = pos;
    }
}
