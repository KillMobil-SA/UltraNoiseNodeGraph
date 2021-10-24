using System;

namespace NoiseUltra.Nodes
{
    public abstract class SampleInfoAsync<T> : BaseSampleInfo
    {
        private int index;
        private float x;
        private float y;
        private T[] values;

        public override void Execute(Func<float, float, float> sampleFunction)
        {
            var sample = sampleFunction(x, y);
            values[index] = Create(sample);
        }

        protected abstract T Create(float sample);

        protected SampleInfoAsync(float x, float y, int index, ref T[] values)
        {
            this.x = x;
            this.y = y;
            this.index = index;
            this.values = values;
        }

        public override string ToString()
        {
            return $"x: {x}|" +
                   $"y: {y}|" +
                   $"index: {index}|" +
                   $"values: {values}|";
        }
    }
}