using System;
using UnityEngine;
using Random = System.Random;
using Sirenix.OdinInspector;

namespace NoiseUltra.Tools.Placement 
{
    [Serializable]
    public class ScaleSettings:PlacementProperties
    {
        [SerializeField] private float size = 1;
        [SerializeField] private bool hasIndividualScale;
        [SerializeField] private ScaleRange dynamicScaleRange;
        [SerializeField] private ScaleRange randomScaleRange;

[Button]
        public override void OnEnable()
        {
 
            
            placementValueRange.rangeType = RangeType.MinMax;
            placementRandomizedRange.rangeType = RangeType.MinMax;
                Debug.Log("OnEnable - >hasIndividualScale:" + hasIndividualScale);
            if (hasIndividualScale)
            {
                placementValueRange.axisType = AxisType.Separated;
                placementRandomizedRange.axisType = AxisType.Separated;
                
                
                placementValueRange.rangeV3 = dynamicScaleRange.maxSizeV3;
                placementValueRange.minRangeV3 = dynamicScaleRange.minSizeV3;
                
                placementRandomizedRange.rangeV3 = randomScaleRange.maxSizeV3;
                placementRandomizedRange.minRangeV3 = randomScaleRange.minSizeV3;
            }
            else
            {
                placementValueRange.axisType = AxisType.Unified;
                placementRandomizedRange.axisType = AxisType.Unified;
                
                placementValueRange.range = dynamicScaleRange.maxSizeV3.x;
                placementValueRange.minRange = dynamicScaleRange.minSizeV3.x;
                placementRandomizedRange.range = randomScaleRange.maxSizeV3.x;
                placementRandomizedRange.minRange = randomScaleRange.minSizeV3.x;
            }
        }

        public override Vector3 Calculator(Vector3 pos, float thresHold)
        {
           var dynamicScale = placementValueRange.GetVectorRange(pos, thresHold);
           var randomScale = placementRandomizedRange.GetVectorRange(pos, thresHold);

           /*
            var dynamicScale = Vector3.zero;
            var randomScale = Vector3.zero;
            
            if (hasIndividualScale)
            {
                // Dynamic Scale
                dynamicScale = dynamicScaleRange.GetPercSizeVector3(thresHold);
                //Random Scale
                randomScale = RandomScale();
            }
            else
            {
                // Dynamic Scale
                var size = dynamicScaleRange.GetPercSizeFloat(thresHold);
                dynamicScale = new Vector3(size, size, size);
                //Random Scale 
                randomScale = randomScaleRange.GetPercSizeVector3((float)random.NextDouble());
            }
            
            */
            var ScaleResult = (dynamicScale + randomScale) * size;
            return ScaleResult;
        }


        private Vector3 RandomScale()
        {
            
            var xRandomScale = randomScaleRange.GetPercSizeFloat((float)random.NextDouble() , 0);
            var yRandomScale = randomScaleRange.GetPercSizeFloat((float)random.NextDouble() , 1);
            var zRandomScale = randomScaleRange.GetPercSizeFloat((float)random.NextDouble() , 2);
            return new Vector3(xRandomScale, yRandomScale, zRandomScale);
        }


        [Serializable]
        public class ScaleRange
        {
            [SerializeField] public  Vector3 minSizeV3 = Vector3.one;
            [SerializeField] public  Vector3 maxSizeV3 = Vector3.one;

            public float GetPercSizeFloat(float v, int axis = 0)
            {
                return Mathf.Lerp(minSizeV3[axis], maxSizeV3[axis], v);
            }

            public Vector3 GetPercSizeVector3(float v)
            {
                return Vector3.Lerp(minSizeV3, maxSizeV3, v);
            }
        }
    }
}
