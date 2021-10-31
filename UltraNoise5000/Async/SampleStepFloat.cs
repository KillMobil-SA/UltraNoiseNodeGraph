using System;

namespace NoiseUltra.Nodes
{
    public sealed class SampleStepFloat : BaseSampleStepAsync<float>
    {
        public SampleStepFloat(float x, float y, int index, ref float[] values) : base(x, y, index, ref values)
        {
        }

        protected override float Create(float sample)
        {
            return sample;
        }
    }
}