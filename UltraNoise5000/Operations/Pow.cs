using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Operations
{
    public class Pow : NodeDoubleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return Mathf.Pow(strengthenedA, strengthenedA);
        }
    }
}