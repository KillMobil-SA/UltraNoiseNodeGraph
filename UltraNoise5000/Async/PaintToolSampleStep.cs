namespace NoiseUltra.Nodes
{
    public sealed class PaintToolSampleStep : SampleStepFloat3
    {
        private readonly float m_AngleV;

        public PaintToolSampleStep(float x, float y, int pxIndex, int pyIndex, int pzIndex, ref float[,,] values, float angleV) :
            base(x, y, pxIndex, pyIndex, pzIndex, ref values)
        {
            m_AngleV = angleV;
        }

        protected override float Create(float sample)
        {
            return m_AngleV * sample;
        }
    }
}