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
        public override Vector3 Calculator(Vector3 pos, float thresHold)
        {
            var valueRotation = placementValueRange.GetVectorRange(pos, thresHold);
            var randomRotation = placementRandomizedRange.GetVectorRange(pos, thresHold);
            var rotationResult = (valueRotation + randomRotation) ;
            return rotationResult;
        }


  
    }
}