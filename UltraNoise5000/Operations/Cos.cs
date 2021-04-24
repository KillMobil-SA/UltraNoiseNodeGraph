using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Operations
{
    public class Cos : NodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Cos(sample);
        }
    }
}