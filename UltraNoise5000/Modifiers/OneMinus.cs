using NoiseUltra.Nodes;

namespace NoiseUltra.Modifiers
{
    public class OneMinus : NodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return 1 - sample;
        }
    }
}