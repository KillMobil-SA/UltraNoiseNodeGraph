using NoiseUltra.Nodes;

namespace NoiseUltra.Utilities 
{
    [NodeTint(NodeColor.PURPLE)]
    public class Coupler : BaseNodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return sample;
        }
    }
}