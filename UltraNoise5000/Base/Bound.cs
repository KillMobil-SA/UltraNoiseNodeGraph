using System;
using UnityEngine;

namespace NoiseUltra
{
    [Serializable]
    public struct Bound
    {
        private const float MinRange = 0;
        private const float MaxRange = 1;
		
        [Range(MinRange, MaxRange)] 
        public float min;
		
        [Range(MinRange, MaxRange)]
        public float max;

        public Bound(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }
}