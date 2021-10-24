using System;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    public sealed class SampleInfoColorAsync : SampleInfoAsync<Color>
    {
        public SampleInfoColorAsync(float x, float y, int index, ref Color[] values, Action onComplete) : base(x, y, index, ref values, onComplete)
        {
        }

        protected override Color Create(float sample)
        {
            return new Color(sample, sample, sample);
        }
    }
}