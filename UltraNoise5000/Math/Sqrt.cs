using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Math
{
    [NodeTint(NodeColor.MATH)]
    public class Sqrt : BaseNodeModifier
    {
        
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Sqrt(sample);
        }
    }
}