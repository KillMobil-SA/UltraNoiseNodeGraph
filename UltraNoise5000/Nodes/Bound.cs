using System;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    [Serializable]
    public class Bound
    {
        private const float MinRange = 0;
        private const float MaxRange = 1;

        [Range(MinRange, MaxRange)] public float min;

        [Range(MinRange, MaxRange)] public float max;

        public void ResetBounds()
        {
            min = MaxRange;
            max = MinRange;
        }
    }
}