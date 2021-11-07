using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public abstract class BasePlacementSetting
    {
        [InlineProperty]
        [HideLabel]
        [TitleGroup("Value Range")]
        [OnValueChanged(nameof(UpdateTool))]
        public PlacementVectorRangeValue placementValueRange;

        [InlineProperty]
        [HideLabel]
        [TitleGroup("Random Range")]
        [OnValueChanged(nameof(UpdateTool))]
        public PlacementVectorRangeRandom placementRandomizedRange = new PlacementVectorRangeRandom();

        protected PlacementTool m_PlacementTool;

        public virtual void Initialize(PlacementTool placementTool)
        {
            m_PlacementTool = placementTool;
            placementValueRange.Initialize(m_PlacementTool);
            placementRandomizedRange.Initialize(m_PlacementTool);
        }

        public virtual void OnEnable()
        {
        }

        public virtual Vector3 Execute(Vector3 pos, float threshold)
        {
            return Vector3.zero;
        }

        protected void UpdateTool()
        {
            if (m_PlacementTool != null)
            {
                m_PlacementTool.PerformPlacement();
            }
        }
    }
}