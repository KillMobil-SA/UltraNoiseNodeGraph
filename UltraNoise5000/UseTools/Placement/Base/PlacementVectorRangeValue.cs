using System;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class PlacementVectorRangeValue : PlacementVectorRange
    {
        public override Vector3 GetVectorRange(Vector3 pos, float threshold)
        {
            float value = useExternalNoise ? externalSource.GetSample(pos.x, pos.y, pos.z) : threshold;
            bool isUnified = axisType == AxisType.Unified;
            return  isUnified ? GetUnifiedValue(value) : GetSeparatedValue(value);
        }

        private Vector3 GetUnifiedValue(float value)
        {
            if (rangeType == RangeType.MinusPlus)
            {
                minRange = -range;
            }

            float lerpValue = Mathf.Lerp(minRange, range, value);
            float roundValue = RoundValue(lerpValue);
            Vector3 resultVector = new Vector3(roundValue, roundValue, roundValue);
            return resultVector;
        }
        
        private Vector3 GetSeparatedValue(float value)
        {
            if (rangeType == RangeType.MinusPlus)
            {
                minRangeV3 = -rangeV3;
            }

            Vector3 resultVector = RoundValue(Vector3.Lerp(minRangeV3, rangeV3, value));
            return resultVector;
        }
    }
}