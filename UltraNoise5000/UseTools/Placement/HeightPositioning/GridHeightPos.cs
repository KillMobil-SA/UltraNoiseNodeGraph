using System;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class GridHeightPos : HeightBase
    {
        public override bool HeightCheck(Vector3 pos)
        {
            return true;
        }

        public override float GetHeightPos(Vector3 pos)
        {
            return pos.y;
        }
    }
}