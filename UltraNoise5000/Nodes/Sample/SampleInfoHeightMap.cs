using System;

namespace NoiseUltra.Nodes
{
    public sealed class SampleInfoHeightMap : SampleInfo2DAsync<float>
    {
        public SampleInfoHeightMap(float x, float y, int pxIndex, int pyIndex, ref float[,] values) : 
            base(x, y, pxIndex, pyIndex, ref values)
        {
        }

        protected override float Create(float sample)
        {
            return sample;
        }
    }
}