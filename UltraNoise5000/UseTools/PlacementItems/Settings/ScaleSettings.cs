using System;
using UnityEngine;
using Random = System.Random;
using Sirenix.OdinInspector;

namespace NoiseUltra.Tools.Placement 
{
    [Serializable]
    public class ScaleSettings:PlacementProperties
    {
        #region Member
        [SerializeField] private float size = 1;
        #endregion
        
        #region Public
        public override Vector3 Calculator(Vector3 pos, float thresHold)
        {
           var dynamicScale = placementValueRange.GetVectorRange(pos, thresHold);
           var randomScale = placementRandomizedRange.GetVectorRange(pos, thresHold);
           var ScaleResult = (dynamicScale + randomScale) * size;
            return ScaleResult;
        }
        #endregion
        
    }
}
