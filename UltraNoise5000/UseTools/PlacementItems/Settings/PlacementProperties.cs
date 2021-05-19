using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    public class PlacementProperties
    {
        // Start is called before the first frame update
        public int seed;
        
        protected const int DemDevide = 1000;
        
        public virtual void InitPropertie()
        {
            random = new Random(seed);
        }
        
        public virtual Vector3 Calculator(Vector3 pos, float thresHold)
        {
            return Vector3.zero;
        }

        private  Random _random;

        protected Random random
        {
            get
            {
                if (_random == null)
                    _random = new Random(seed);
                return _random;
            }
            set { _random = value; }
        }


    }
}