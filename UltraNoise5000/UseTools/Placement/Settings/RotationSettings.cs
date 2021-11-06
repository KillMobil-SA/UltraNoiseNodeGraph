using System;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class RotationSettings :BasePlacementSetting
    {
        public override Vector3 Execute(Vector3 pos, float threshold)
        {
            Vector3 valueRotation = placementValueRange.GetVectorRange(pos, threshold);
            Vector3 randomRotation = placementRandomizedRange.GetVectorRange(pos, threshold);
            Vector3 rotationResult = (valueRotation + randomRotation) ;
            return rotationResult;
        }
    }
}