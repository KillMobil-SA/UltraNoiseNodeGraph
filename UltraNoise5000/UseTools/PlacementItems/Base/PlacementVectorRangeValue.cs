using System.Collections;
using System.Collections.Generic;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    [System.Serializable]
    public class PlacementVectorRangeValue : PlacementVectorRange
    {

        public override Vector3 GetVectorRange(Vector3 pos, float thresHold)
        {
            var value = useExternalNoise ? externalSource.GetSample(pos.x, pos.y, pos.z) : thresHold;
            return (axisType == AxisType.Unified ? UnifiedValueVector(value) : SeparatedValueVector(value));
        }
        
        private Vector3 UnifiedValueVector (float value) {
                Vector3 resultVector;
                if(rangeType == RangeType.MinusPlus)
                    minRange = -range;
                float val = RoundValue (Mathf.Lerp(minRange, range, value));
                
                resultVector = new Vector3(val, val, val);
                return resultVector;
        }


        private Vector3 SeparatedValueVector (float value) {
             Vector3 resultVector;
             if(rangeType == RangeType.MinusPlus)
                    minRangeV3 = -rangeV3;
                
             resultVector = RoundValue(Vector3.Lerp(minRangeV3, rangeV3, value));
             return resultVector;
        }
        
        
    }
}