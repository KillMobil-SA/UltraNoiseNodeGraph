using System;

namespace NoiseUltra.Nodes
{
    public abstract class SampleInfo2DAsync<T> : BaseSampleInfo
    {
        private readonly int m_PxIndex;
        private readonly int m_PyIndex;
        private readonly float m_X;
        private readonly float m_Y;
        private readonly T[,] m_Values;
        
        public override void Execute(Func<float, float, float> sampleFunction)
        {
            float sample = sampleFunction(m_X, m_Y);
            m_Values[m_PxIndex, m_PyIndex] = Create(sample);
        }

        protected abstract T Create(float sample);

        protected SampleInfo2DAsync(float x, float y, int pxIndex, int pyIndex, ref T[,] values)
        {
            m_X = x;
            m_Y = y;
            m_PxIndex = pxIndex;
            m_PyIndex = pyIndex;
            m_Values = values;
        }

        public override string ToString()
        {
            return $"x: {m_X}|" +
                   $"y: {m_Y}|" +
                   $"pxIndex: {m_PxIndex}|" +
                   $"pyIndex: {m_PyIndex}|" +
                   $"values: {m_Values}|";
        }
    }
}