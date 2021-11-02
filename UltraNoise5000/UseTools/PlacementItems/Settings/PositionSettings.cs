using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class PositionSettings :PlacementProperties
    {
        public Vector3 randomPositioning;

        [Header("Height Calulations Settings")] [OnValueChanged(nameof(UpdateHeightInterFace))] [SerializeField]
        private HeightPosType heightPosType = HeightPosType.Grid;

        [ShowIf(nameof(heightPosType), HeightPosType.Raycast)] [SerializeField]
        private RayCastHeightPos rayCastHeightPos = new RayCastHeightPos();

        [ShowIf("heightPosType", HeightPosType.Noise)] [SerializeField]
        private NoiseHeightPos noiseHeightPos = new NoiseHeightPos();

        [SerializeField] private IHeightBase _heightBase;
        private GridHeightPos noiseGridPos = new GridHeightPos();


        public PositionSettings()
        {
            UpdateHeightInterFace();
        }

        public override void OnEnable()
        {
            UpdateHeightInterFace();
            placementValueRange.axisType = AxisType.Separated;
            placementRandomizedRange.axisType = AxisType.Separated;
            
            placementValueRange.rangeType = RangeType.MinusPlus;
            placementRandomizedRange.rangeType = RangeType.MinusPlus;
            
            placementRandomizedRange.rangeV3 = randomPositioning;
            placementRandomizedRange.minRangeV3 = -randomPositioning;
        }


        private void UpdateHeightInterFace()
        {
            //_heightBase = noiseGridPos;
            switch (heightPosType)
            {
                case HeightPosType.Grid:
                    _heightBase = noiseGridPos;
                    break;
                case HeightPosType.Noise:
                    _heightBase = noiseHeightPos;
                    break;
                case HeightPosType.Raycast:
                    _heightBase = rayCastHeightPos;
                    break;
                default:
                    _heightBase = noiseGridPos;
                    break;
            }
        }

        public override Vector3 Calculator(Vector3 pos, float thresHold)
        {
            var sourceHeightCalculation = new Vector3(pos.x, CalculateHeight(pos), pos.z);
            
            var valuePosition = placementValueRange.GetVectorRange(pos, thresHold);
            var randomPosition = placementRandomizedRange.GetVectorRange(pos, thresHold);

            var positionResult = sourceHeightCalculation +  (valuePosition + randomPosition) ;
            return positionResult;
            
            
        }

        private float CalculateHeight(Vector3 pos)
        {
            return _heightBase.GetHeightPos(pos);
 
        }

        public bool IsPositionValid(Vector3 pos)
        {
            return _heightBase.HeightCheck(pos);
        }
    }
}


/*  /// i keep this commented area Until we evaluate  current Height pos Implementation
 // Should we have different class for each type of height positioning of should we just throw all this here?
 switch (heightPosType)
 {
     case HeightPosType.Grid:
         return noiseGridPos.GetHeightPos(pos);
     case HeightPosType.Noise:
         return noiseHeightPos.GetHeightPos(pos);
     case HeightPosType.Raycast:
         return rayCastHeightPos.GetHeightPos(pos);
     default:
         return pos.y;
 }
 */