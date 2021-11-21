using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Tools.Placement
{
    public abstract class BasePlacementSetting
    {
        [InlineProperty]
        [HideLabel]
        [TitleGroup("Value Range")]
        public PlacementVectorRangeValue placementValueRange = new PlacementVectorRangeValue();

        [InlineProperty]
        [HideLabel]
        [TitleGroup("Random Range")]
        public PlacementVectorRangeRandom placementRandomizedRange = new PlacementVectorRangeRandom();

        public BasePlacementSetting(float initialRangeValue, float initialRangeRandom)
        {
            placementValueRange.minRange = initialRangeValue;
            placementValueRange.range = initialRangeValue;
            
            placementRandomizedRange.minRange = initialRangeRandom;
            placementRandomizedRange.range = initialRangeRandom;
        }

  
        public virtual void Initialize()
        {
            placementValueRange.Initialize();
            placementRandomizedRange.Initialize();
        }

        public virtual void OnEnable()
        {
        }

        public virtual Vector3 Execute(Vector3 pos, float threshold)
        {
            return Vector3.zero;
        }
    }
}