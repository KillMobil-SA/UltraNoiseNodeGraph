using System;

namespace NoiseUltra.Nodes
{
    /// <summary>
    /// A simple step in the async sample generation.
    /// </summary>
    public abstract class BaseSampleStep
    {
        public abstract void Execute(Func<float, float, float> sampleFunction);
    }
}