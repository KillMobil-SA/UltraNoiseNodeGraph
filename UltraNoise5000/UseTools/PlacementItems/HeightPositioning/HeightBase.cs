using System;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public abstract class HeightBase : IHeightBase
    {
        public abstract bool HeightCheck(Vector3 pos);

        public abstract float GetHeightPos(Vector3 pos);
    }
}