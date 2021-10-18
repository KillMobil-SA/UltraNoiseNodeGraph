using System;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    [Serializable]
    public sealed class Bound
    {
        private const float MinRange = 0;
        private const float MaxRange = 1;

        [Range(MinRange, MaxRange)] 
        public float min;

        [Range(MinRange, MaxRange)] 
        public float max;

        public void Reset()
        {
            min = MaxRange;
            max = MinRange;
        }
    }
}