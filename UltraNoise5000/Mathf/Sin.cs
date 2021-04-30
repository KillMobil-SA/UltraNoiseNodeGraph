using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Math
{
    public class Sin : NodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Sin(sample);
        }
    }
}