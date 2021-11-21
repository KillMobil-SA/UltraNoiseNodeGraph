using System;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class ScaleSettings : BasePlacementSetting
    {
        [SerializeField] 
        private float size = 1;
        
        public override Vector3 Execute(Vector3 pos, float threshold)
        {
            Vector3 dynamicScale = placementValueRange.GetVectorRange(pos, threshold);
            Vector3 randomScale = placementRandomizedRange.GetVectorRange(pos, threshold);
            Vector3 result = (dynamicScale + randomScale) * size;
            return result;
        }

        public ScaleSettings(float initialRangeValue, float initialRangeRandom) : base(initialRangeValue, initialRangeRandom)
        {
        }
    }
}
