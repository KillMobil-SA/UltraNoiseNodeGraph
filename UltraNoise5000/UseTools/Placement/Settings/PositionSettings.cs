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
        
        public PositionSettings(float initialRangeValue, float initialRangeRandom) : base(initialRangeValue, initialRangeRandom)
        {
            UpdateHeightInterFace();
        }
        
        public override Vector3 Execute(Vector3 pos, float threshold)
        {
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
        }

        private float CalculateHeight(Vector3 pos)
        {
            return m_HeightBase.GetHeightPos(pos);
 
        }
    }
}

