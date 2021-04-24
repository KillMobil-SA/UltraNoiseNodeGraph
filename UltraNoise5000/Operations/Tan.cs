using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Operations
{
    public class Tan : NodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Tan(sample);
        }
    }
}