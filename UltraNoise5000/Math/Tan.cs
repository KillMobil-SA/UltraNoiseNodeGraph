using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Math
{
    public class Tan : BaseNodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return Mathf.Tan(sample);
        }
    }
}