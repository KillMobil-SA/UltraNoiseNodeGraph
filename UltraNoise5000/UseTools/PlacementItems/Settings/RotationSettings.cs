using System;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class RotationSettings :PlacementProperties
    {
        
        [SerializeField] private Vector3 randomRotation;
        [SerializeField] private bool useNoiseRandomizationBase;
        [ShowIf("useNoiseRandomizationBase")] [SerializeField]
        private bool useExternalNoiseSource;

        [ShowIf("useExternalNoiseSource")] [SerializeField]
        private NodeBase externalSource;

        public bool roundRotation;

        [ShowIf("roundRotation")] [SerializeField]
        private float rotationRound;

        
        public override void OnEnable()
        {
                        
                placementValueRange.axisType = AxisType.Separated;
                placementRandomizedRange.axisType = AxisType.Separated;
                
                placementValueRange.rangeType = RangeType.MinusPlus;
                placementRandomizedRange.rangeType = RangeType.MinusPlus;
                
                placementRandomizedRange.rangeV3 = randomRotation;
                placementRandomizedRange.minRangeV3 = -randomRotation;
        }

        public override Vector3 Calculator(Vector3 pos, float thresHold)
        {
            
            var valueRotation = placementValueRange.GetVectorRange(pos, thresHold);
            var randomRotation = placementRandomizedRange.GetVectorRange(pos, thresHold);

            var rotationResult = (valueRotation + randomRotation) ;
            return rotationResult;
            

        }


  
    }
}