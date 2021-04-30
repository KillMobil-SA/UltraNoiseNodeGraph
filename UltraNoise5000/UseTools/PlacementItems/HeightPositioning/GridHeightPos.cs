using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridHeightPos
{
    public bool HeightCheck(Vector3 pos)
    {
        return true;
    }

    public float GetHeightPos(Vector3 pos)
    {
        return pos.y;
    }
}