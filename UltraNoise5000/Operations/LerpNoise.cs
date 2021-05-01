using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Operations
{
    public class LerpNoise : NodeTripleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB, float strengthenedC)
        {
            return Mathf.Lerp(strengthenedA, strengthenedB, strengthenedC);
        }
    }
}