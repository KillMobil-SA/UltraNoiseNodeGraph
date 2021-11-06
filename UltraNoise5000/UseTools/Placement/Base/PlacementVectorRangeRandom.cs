using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace NoiseUltra.Tools.Placement
{
    [System.Serializable]
    public class PlacementVectorRangeRandom : PlacementVectorRange
    {
        #region Public

        [BoxGroup("Random")]
        public int seed;
        
        [BoxGroup("Random")]
        public bool usePosAsRandomSeed;

        public override void Initialize()
        {
            if (seed == 0)
            {
                seed = UnityEngine.Random.Range(0, MAXRandomSeedValue);
                Debug.Log("PlacementVectorRangeRandom  new seed:" + seed);
            }

            random = new Random(seed);
        }

        public override Vector3 GetVectorRange(Vector3 pos, float threshold)
        {
            if (usePosAsRandomSeed)
            {
                InitRandomizationSeedPosition(pos, threshold);
            }

            return axisType == AxisType.Unified ? UnifiedRandomVector() : SeparateRandomVector();
        }

        #endregion

        #region Private

        private Vector3 UnifiedRandomVector()
        {
            Vector3 resultVector;
            if (rangeType == RangeType.MinusPlus)
                minRange = -range;

            float val = RoundValue(
                (float) random.Next(Mathf.RoundToInt(minRange * Divider), Mathf.RoundToInt(range * Divider)) /
                Divider);

            resultVector = new Vector3(val, val, val);
            return resultVector;
        }

        private Vector3 SeparateRandomVector()
        {
            if (rangeType == RangeType.MinusPlus)
                minRangeV3 = -rangeV3;

            var xPos = (float) random.Next(Mathf.RoundToInt(minRangeV3.x * Divider),
                Mathf.RoundToInt(rangeV3.x * Divider)) / Divider;
            var yPos = (float) random.Next(Mathf.RoundToInt(minRangeV3.y * Divider),
                Mathf.RoundToInt(rangeV3.y * Divider)) / Divider;
            var zPos = (float) random.Next(Mathf.RoundToInt(minRangeV3.z * Divider),
                Mathf.RoundToInt(rangeV3.z * Divider)) / Divider;

            var resultVector = RoundValue(new Vector3(xPos, yPos, zPos));
            return resultVector;
        }



        private void InitRandomizationSeedPosition(Vector3 pos, float thresHold)
        {
            float v = useExternalNoise ? externalSource.GetSample(pos.x, pos.y, pos.z) : thresHold;

            random = new Random(Mathf.RoundToInt(v * MAXRandomSeedValue));
        }

        #endregion

        #region Protected

        protected const int MAXRandomSeedValue = 9999;
        protected const int Divider = 1000;

        protected Random _random;

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

        #endregion
    }
}