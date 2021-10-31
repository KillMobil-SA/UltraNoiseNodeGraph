namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Class samples a value and applies a modifier.
    /// </summary>
    public abstract class BaseNodeModifier : NodeInputOutput
    {
        protected abstract float ApplyModifier(float sample);

        public override float GetSample(float x)
        {
            float sample = base.GetSample(x);
            return ApplyModifier(sample);
        }

        public override float GetSample(float x, float y)
        {
            float sample = base.GetSample(x, y);
            return ApplyModifier(sample);
        }

        public override float GetSample(float x, float y, float z)
        {
            float sample = base.GetSample(x, y, z);
            return ApplyModifier(sample);
        }
    }
}