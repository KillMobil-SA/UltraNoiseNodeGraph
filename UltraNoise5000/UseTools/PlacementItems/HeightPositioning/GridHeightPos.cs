using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [System.Serializable]
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
