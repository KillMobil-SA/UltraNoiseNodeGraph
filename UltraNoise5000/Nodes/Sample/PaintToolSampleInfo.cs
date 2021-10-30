namespace NoiseUltra.Nodes
{
    public sealed class PaintToolSampleInfo : SampleInfo3DAsync<float>
    {
        private readonly float m_AngleV;

        public PaintToolSampleInfo(float x, float y, int pxIndex, int pyIndex, int pzIndex, ref float[,,] values, float angleV) :
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