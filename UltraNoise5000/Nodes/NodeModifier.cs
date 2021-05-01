namespace NoiseUltra.Nodes
{
    public abstract class NodeModifier : NodeInputOutput
    {
        public override float GetSample(float x)
        {
            var sample = base.GetSample(x);
            return ApplyModifier(sample);
        }

        public override float GetSample(float x, float y)
        {
            var sample = base.GetSample(x, y);
            return ApplyModifier(sample);
        }

        public override float GetSample(float x, float y, float z)
        {
            var sample = base.GetSample(x, y, z);
            return ApplyModifier(sample);
        }

        protected abstract float ApplyModifier(float sample);
    }
}