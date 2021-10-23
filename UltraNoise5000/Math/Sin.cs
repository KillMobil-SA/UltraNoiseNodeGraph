using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Math
{
    public class Sin : BaseNodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Sin(sample);
        }
    }
}