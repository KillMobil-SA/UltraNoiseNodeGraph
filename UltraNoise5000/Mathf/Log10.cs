using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Math
{
    public class Log10 : NodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Log10(sample);
        }
    }
}