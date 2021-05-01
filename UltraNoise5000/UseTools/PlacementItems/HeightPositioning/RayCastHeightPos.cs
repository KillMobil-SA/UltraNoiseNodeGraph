using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoiseUltra.Tools.Placement
{
[System.Serializable]
public class RayCastHeightPos: HeightBase
{

    [Header("Height Settings")]
    public string tagCheck = "Ground";
    public LayerMask layerMask;
    public bool showDebug;
    RaycastHit hit;

    public override float GetHeightPos(Vector3 pos)
    {
        
        return hit.point.y;
    }

    public override bool HeightCheck(Vector3 pos)
    {
        return RayCastHitTest(pos);
    }

    public bool RayCastHitTest(Vector3 pos)
    {
        Vector3 raycastStartPoint = pos;
        raycastStartPoint.y += 500;

        if (Physics.Raycast(raycastStartPoint, Vector3.down, out hit, Mathf.Infinity, layerMask))
        {
            if (tagCheck == "")
                return DrawDebug(true, raycastStartPoint, hit.point);
            else if (tagCheck != "" && tagCheck == hit.transform.tag)
                return DrawDebug(true, raycastStartPoint, hit.point);
            else
                return DrawDebug(false, raycastStartPoint, raycastStartPoint + Vector3.down * 100);
        }
        else
            return DrawDebug(false, raycastStartPoint, raycastStartPoint + Vector3.down * 100);
    }

    public bool DrawDebug(bool hasHitted, Vector3 raycastStartPoint, Vector3 rayCastEndPoint)
    {
        if (showDebug)
        {
            if (hasHitted) Debug.DrawLine(raycastStartPoint, rayCastEndPoint, Color.green);
            else Debug.DrawLine(raycastStartPoint, rayCastEndPoint, Color.red);
        }

        return hasHitted;
    }
}

}
