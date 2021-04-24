using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Operations
{
    public class Div : NodeDoubleInputOutput
    {
        public static float NaNCheck(float a) => Mathf.Approximately(0f, a) ? Mathf.Epsilon : a;
        
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return strengthenedA / NaNCheck(strengthenedB);
        }
    }
}