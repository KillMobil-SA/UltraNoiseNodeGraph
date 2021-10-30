using System;

namespace NoiseUltra.Nodes
{
    public abstract class SampleInfo3DAsync<T> : BaseSampleInfo
    {
        private readonly int m_PxIndex;
        private readonly int m_PyIndex;
        private readonly int m_PzIndex;
        private readonly float m_X;
        private readonly float m_Y;
        private readonly T[,,] m_Values;

        public override void Execute(Func<float, float, float> sampleFunction)
        {
            float sample = sampleFunction(m_X, m_Y);
            m_Values[m_PxIndex, m_PyIndex, m_PzIndex] = Create(sample);
        }

        protected abstract T Create(float sample);

        protected SampleInfo3DAsync(float x, float y, int pxIndex, int pyIndex, int pzIndex, ref T[,,] values)
        {
            m_X = x;
            m_Y = y;
            m_PxIndex = pxIndex;
            m_PyIndex = pyIndex;
            m_PzIndex = pzIndex;
            m_Values = values;
        }

        public override string ToString()
        {
            return $"x: {m_X}|" +
                   $"y: {m_Y}|" +
                   $"pxIndex: {m_PxIndex}|" +
                   $"pyIndex: {m_PyIndex}|" +
                   $"pzIndex: {m_PzIndex}|" +
                   $"values: {m_Values}|";
        }
    }
}