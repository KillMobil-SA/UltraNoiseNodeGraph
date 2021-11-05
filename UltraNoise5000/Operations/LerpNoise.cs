using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Operations
{
    [NodeTint(NodeColor.OPERATION)]
    public class LerpNoise : NodeTripleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB, float strengthenedC)
        {
            return Mathf.Lerp(strengthenedA, strengthenedB, strengthenedC);
        }
    }
}