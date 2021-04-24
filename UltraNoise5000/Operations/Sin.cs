using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Operations
{
    public class Sin : NodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Sin(sample);
        }
    }
}