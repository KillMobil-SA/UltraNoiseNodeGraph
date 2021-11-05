using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Math
{
    [NodeTint(NodeColor.MATH)]
    public class Pow : NodeDoubleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return Mathf.Pow(strengthenedA, strengthenedA);
        }
    }
}