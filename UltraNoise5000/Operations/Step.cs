using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Operations
{
    public class Step : NodeModifier
    {
        private const int MinSteps = 1;
        private const int MaxSteps = 10;
        
        [SerializeField, Range(MinSteps, MaxSteps)]
        private int steps = MinSteps;

        protected override float ApplyModifier(float sample)
        {
            return Mathf.Floor(steps * sample) / steps;
        }
    }
}