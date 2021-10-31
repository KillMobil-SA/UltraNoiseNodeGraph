namespace NoiseUltra.Nodes
{
    public class SampleStepFloat2 : BaseSampleStep2DAsync<float>
    {
        public SampleStepFloat2(float x, float y, int pxIndex, int pyIndex, ref float[,] values) : 
            base(x, y, pxIndex, pyIndex, ref values)
        {
        }

        protected override float Create(float sample)
        {
            return sample;
        }
    }
}