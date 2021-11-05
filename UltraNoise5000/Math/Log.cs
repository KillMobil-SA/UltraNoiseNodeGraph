using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Math
{
    [NodeTint(NodeColor.MATH)]
    public class Log : NodeDoubleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return Mathf.Log(strengthenedA, strengthenedA);
        }
    }
}