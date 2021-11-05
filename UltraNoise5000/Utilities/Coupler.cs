using NoiseUltra.Nodes;

namespace NoiseUltra.Utilities 
{
    [NodeTint(NodeColor.UTILITY)]
    public class Coupler : BaseNodeModifier
    {
        protected override float ApplyModifier(float sample)
        {
            return sample;
        }
    }
}