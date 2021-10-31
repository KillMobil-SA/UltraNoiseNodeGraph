using System;

namespace NoiseUltra.Nodes
{
    public abstract class BaseSampleStepAsync<T> : BaseSampleStep
    {
        private readonly int m_Index;
        private readonly float m_X;
        private readonly float m_Y;
        private readonly T[] m_Values;

        protected BaseSampleStepAsync(float x, float y, int index, ref T[] values)
        {
            m_X = x;
            m_Y = y;
            m_Index = index;
            m_Values = values;
        }

        public override void Execute(Func<float, float, float> sampleFunction)
        {
            float sample = sampleFunction(m_X, m_Y);
            m_Values[m_Index] = Create(sample);
        }

        protected abstract T Create(float sample);

        public override string ToString()
        {
            return $"x: {m_X}|" +
                   $"y: {m_Y}|" +
                   $"index: {m_Index}|" +
                   $"values: {m_Values}|";
        }
    }
}