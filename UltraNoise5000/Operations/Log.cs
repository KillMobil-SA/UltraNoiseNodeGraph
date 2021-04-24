using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Operations
{
    public class Log : NodeDoubleInputOutput
    {
        protected override float ExecuteOperation(float strengthenedA, float strengthenedB)
        {
            return Mathf.Log(strengthenedA, strengthenedA);
        }
    }
}