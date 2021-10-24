using System;

namespace NoiseUltra.Nodes
{
    public sealed class SampleInfoHeightMap : SampleInfo2DAsync<float>
    {
        public SampleInfoHeightMap(float x, float y, int xMax, int yMax, int pxIndex, int pyIndex, ref float[,] values, Action onComplete) : 
            base(x, y, xMax, yMax, pxIndex, pyIndex, ref values, onComplete)
        {
        }

        protected override float Create(float sample)
        {
            return sample;
        }
    }
}