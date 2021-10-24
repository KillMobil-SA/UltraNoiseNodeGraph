using System;

namespace NoiseUltra.Nodes
{
    public abstract class BaseSampleInfo
    {
        public abstract void Execute(Func<float, float, float> sampleFunction);
    }
}