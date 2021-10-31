namespace NoiseUltra.Nodes
{
    public class SampleStepFloat3 : BaseSampleStep3DAsync<float>
    {
        public SampleStepFloat3(float x, float y, int pxIndex, int pyIndex, int pzIndex, ref float[,,] values) :
            base(x, y, pxIndex, pyIndex, pzIndex, ref values)
        {
        }

        protected override float Create(float sample)
        {
            return sample;
        }
    }
}