using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class PositionSettings : PlacementProperties
    {
      
        #region Members

        [TitleGroup("Height Calculation Settings") ,  PropertyOrder(-1)] 
        [OnValueChanged(nameof(UpdateHeightInterFace))] [SerializeField]
        [EnumToggleButtons , HideLabel]        
        private HeightPosType heightPosType = HeightPosType.Grid;
        [BoxGroup("Height") , InlineProperty (), HideLabel , PropertyOrder(-1)]
        [ShowIf(nameof(heightPosType), HeightPosType.Raycast),SerializeField]
        private RayCastHeightPos rayCastHeightPos = new RayCastHeightPos();
        [SerializeField] private IHeightBase _heightBase;
        private GridHeightPos noiseGridPos = new GridHeightPos();
        #endregion

        #region Public
        
        public PositionSettings()
        {
            UpdateHeightInterFace();
        }
        
        public override Vector3 Calculator(Vector3 pos, float thresHold)
        {
            var sourceHeightCalculation = new Vector3(pos.x, CalculateHeight(pos), pos.z);
            
            var valuePosition = placementValueRange.GetVectorRange(pos, thresHold);
            var randomPosition = placementRandomizedRange.GetVectorRange(pos, thresHold);

            var positionResult = sourceHeightCalculation +  (valuePosition + randomPosition) ;
            return positionResult;
        }
        
        public override void OnEnable()
        {
            UpdateHeightInterFace();
        }
        
        public bool IsPositionValid(Vector3 pos)
        {
            return _heightBase.HeightCheck(pos);
        }   
        
        #endregion

        #region Private

        private void UpdateHeightInterFace()
        {
            //_heightBase = noiseGridPos;
            switch (heightPosType)
            {
                case HeightPosType.Grid:
                    _heightBase = noiseGridPos;
                    break;
                case HeightPosType.Raycast:
                    _heightBase = rayCastHeightPos;
                    break;
                default:
                    _heightBase = noiseGridPos;
                    break;
            }
        }

        

        private float CalculateHeight(Vector3 pos)
        {
            return _heightBase.GetHeightPos(pos);
 
        }
        #endregion
        

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