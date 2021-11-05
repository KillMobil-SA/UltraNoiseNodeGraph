using NoiseUltra.Nodes;

namespace NoiseUltra.Modifiers
{
    [NodeTint(NodeColor.MODIFIER)]
    public class OneMinus : BaseNodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return 1 - sample;
        }
    }
}