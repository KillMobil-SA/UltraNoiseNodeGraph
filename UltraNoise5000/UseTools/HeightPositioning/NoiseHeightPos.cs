using System;
using NoiseUltra.Nodes;
using UnityEngine;

[Serializable]
public class NoiseHeightPos
{
    public NodeBase height;
    public float heightAmount;

    public bool HeightCheck(Vector3 pos)
    {
        return true;
    }

    public float GetHeightPos(Vector3 pos)
    {
        if (height)
        {
            var v = height.Sample2D(pos.x, pos.z);
            return v * heightAmount;
        }

        return 0;
    }
}