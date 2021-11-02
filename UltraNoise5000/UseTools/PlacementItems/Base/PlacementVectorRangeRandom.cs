using System.Collections;
using System.Collections.Generic;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    [System.Serializable]
    public class PlacementVectorRangeRandom : PlacementVectorRange
    {
        protected const int MAXRandomSeedValue = 9999;
        protected const int Divider = 1000;

        [BoxGroup("Random")]
        
        public int seed;
        [BoxGroup("Random")]
        public bool usePosAsRandomSeed;

        public override void Init()
        {
            if (seed == 0)
                seed = UnityEngine.Random.Range(0, MAXRandomSeedValue);
            Debug.Log("New Random with seed:" + seed);
            random = new Random(seed);
        }

        public override Vector3 GetVectorRange(Vector3 pos, float thresHold)
        {
            if (usePosAsRandomSeed)
                InitRandomizationSeedPosition(pos, thresHold);
            
            return ((axisType == AxisType.Unified) ? UnifiedRandomVector () : SeparateRandomVector ());
        }
        
        private Vector3 UnifiedRandomVector  () {
               Vector3 resultVector;
                if(rangeType == RangeType.MinusPlus)
                    minRange = -range;

                float val = RoundValue ((float) random.Next(Mathf.RoundToInt(minRange * Divider), Mathf.RoundToInt(range * Divider)) /
                                        Divider);
                
                resultVector = new Vector3(val, val, val);
                return resultVector;
        }


        private Vector3 SeparateRandomVector (){

                Vector3 resultVector;
                if(rangeType == RangeType.MinusPlus)
                    minRangeV3 = -rangeV3;

                var xPos = (float) random.Next(Mathf.RoundToInt(minRangeV3.x * Divider),
                    Mathf.RoundToInt(rangeV3.x * Divider)) / Divider;
                var yPos = (float) random.Next(Mathf.RoundToInt(minRangeV3.y * Divider),
                    Mathf.RoundToInt(rangeV3.y * Divider)) / Divider;
                var zPos = (float) random.Next(Mathf.RoundToInt(minRangeV3.z * Divider),
                    Mathf.RoundToInt(rangeV3.z * Divider)) / Divider;
                
                resultVector = RoundValue (new Vector3(xPos, yPos, zPos));
                return resultVector;
                
        }

        
   
        public void InitRandomizationSeedPosition(Vector3 pos, float thresHold)
        {
            float v;
            if (useExternalNoise)
                v = externalSource.GetSample(pos.x, pos.y, pos.z);
            else
                v = thresHold;
            
            random = new Random(Mathf.RoundToInt(v * MAXRandomSeedValue));
        }
        
        
        private  Random _random;
        protected Random random
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random(seed);
                }

                return _random;
            }
            set { _random = value; }
        }
       


        
    }
}