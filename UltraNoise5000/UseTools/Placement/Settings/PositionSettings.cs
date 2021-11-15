using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    [Serializable]
    public class PositionSettings : BasePlacementSetting
    {
        private const string HEIGHT_NAME = "Height";
        private const string HEIGHT_CALCULATION_SETTINGS = "Height Calculation Settings";

        [TitleGroup(HEIGHT_CALCULATION_SETTINGS)]
        [PropertyOrder(-1)] 
        [OnValueChanged(nameof(UpdateHeightInterFace))]
        [SerializeField]
        [EnumToggleButtons]
        [HideLabel]
        private HeightPosType heightPosType = HeightPosType.Grid;
        
        [BoxGroup(HEIGHT_NAME)]
        [InlineProperty]
        [HideLabel]
        [PropertyOrder(-1)]
        [ShowIf(nameof(heightPosType), HeightPosType.Raycast)]
        [SerializeField]
        private RayCastHeightPos rayCastHeightPos = new RayCastHeightPos();
        
        private GridHeightPos m_NoiseGridPos = new GridHeightPos();
        private IHeightBase m_HeightBase;
        
        public PositionSettings()
        {
            UpdateHeightInterFace();
        }
        
        public override Vector3 Execute(Vector3 pos, float threshold)
        {
            Debug.Log("heightPosType:" + m_HeightBase);
            
            var valuePosition = placementValueRange.GetVectorRange(pos, threshold);
            var randomPosition = placementRandomizedRange.GetVectorRange(pos, threshold);

            var positionResult = (valuePosition + randomPosition) + pos ;

            
            var sourceHeightCalculation = new Vector3(positionResult.x, CalculateHeight(positionResult), positionResult.z);
            
            return sourceHeightCalculation;
        }
        
        public override void OnEnable()
        {
            UpdateHeightInterFace();
            
        }
        
        public bool IsPositionValid(Vector3 pos)
        {
            return m_HeightBase.HeightCheck(pos);
        }   
        
        [Button]
        private void UpdateHeightInterFace()
        {
            Debug.Log("UpdateHeightInterFace()");
            switch (heightPosType)
            {
                case HeightPosType.Grid:
                    m_HeightBase = m_NoiseGridPos;
                    break;
                case HeightPosType.Raycast:
                    m_HeightBase = rayCastHeightPos;
                    break;
                default:
                    m_HeightBase = m_NoiseGridPos;
                    break;
            }
            Debug.Log("heightPosType:" + m_HeightBase);
        }

        private float CalculateHeight(Vector3 pos)
        {
            return m_HeightBase.GetHeightPos(pos);
 
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