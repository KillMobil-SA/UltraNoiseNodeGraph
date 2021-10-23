using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Math
{
    public class Cos : BaseNodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Cos(sample);
        }
    }
}