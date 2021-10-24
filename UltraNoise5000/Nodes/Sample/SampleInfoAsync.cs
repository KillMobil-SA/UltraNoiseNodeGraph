using System;

namespace NoiseUltra.Nodes
{
    public abstract class SampleInfoAsync<T> : BaseSampleInfo
    {
        private readonly int m_Index;
        private readonly float m_X;
        private readonly float m_Y;
        private readonly T[] m_Values;

        public override void Execute(Func<float, float, float> sampleFunction)
        {
            float sample = sampleFunction(m_X, m_Y);
            m_Values[m_Index] = Create(sample);
        }

        protected abstract T Create(float sample);

        protected SampleInfoAsync(float x, float y, int index, ref T[] values)
        {
            this.m_X = x;
            this.m_Y = y;
            this.m_Index = index;
            this.m_Values = values;
        }

        public override string ToString()
        {
            return $"x: {m_X}|" +
                   $"y: {m_Y}|" +
                   $"index: {m_Index}|" +
                   $"values: {m_Values}|";
        }
    }
}