using UnityEngine;

namespace NoiseUltra.Operations
{
    public static class MathUtils
    {
        public static float NaNCheck(float number)
        {
            return Mathf.Approximately(0f, number) ? Mathf.Epsilon : number;
        }
    }
}