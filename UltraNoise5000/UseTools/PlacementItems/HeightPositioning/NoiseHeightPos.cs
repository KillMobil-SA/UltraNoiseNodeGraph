using System;
using NoiseUltra.Output;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class NoiseHeightPos : HeightBase
    {
        public NodeExport height;
        public float heightAmount;

        public override bool HeightCheck(Vector3 pos)
        {
            return true;
        }

        public override float GetHeightPos(Vector3 pos)
        {
            if (height)
            {
                var v = height.Sample2D(pos.x, pos.z);
                return v * heightAmount;
            }

            return 0;
        }
    }
}