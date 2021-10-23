using NoiseUltra.Nodes;

namespace NoiseUltra.Modifiers
{
    public class OneMinus : BaseNodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return 1 - sample;
        }
    }
}