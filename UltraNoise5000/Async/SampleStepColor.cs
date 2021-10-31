using System;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    public sealed class SampleStepColor : BaseSampleStepAsync<Color>
    {
        public SampleStepColor(float x, float y, int index, ref Color[] values) : base(x, y, index, ref values)
        {
        }

        protected override Color Create(float sample)
        {
            return new Color(sample, sample, sample);
        }
    }
}