using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementProperties
    {
        // Start is called before the first frame update
        protected const int DemDevide = 1000;
        [InlineProperty () , HideLabel , TitleGroup("Value Range") ]
        public PlacementVectorRangeValue placementValueRange;
        
        [InlineProperty () , HideLabel , TitleGroup("Random Range") ]
        
        public PlacementVectorRangeRandom placementRandomizedRange;
        
        public virtual void InitProperties()
        {
            placementRandomizedRange.Init();
        }

        public virtual void OnEnable()
        {
        }
        
        public virtual Vector3 Calculator(Vector3 pos, float thresHold)
        {
            return Vector3.zero;
        }

        
       


    }
}