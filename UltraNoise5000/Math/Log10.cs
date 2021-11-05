using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Math
{
    [NodeTint(NodeColor.MATH)]
    public class Log10 : BaseNodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Log10(sample);
        }
    }
}