using UnityEngine;

namespace NoiseUltra.Operations
{
    public static class MathUtils
    {
        public const float PI_TIMES_2 = 2f * Mathf.PI;

        public static float NaNCheck(float number)
        {
            return Mathf.Approximately(0f, number) ? Mathf.Epsilon : number;
        }
    }
}