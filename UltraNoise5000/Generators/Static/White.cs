using NoiseUltra.Nodes;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeColor.GREEN)]
    public class White : NodeOutput
    {
        public override float GetSample(float x)
        {
            return 1;
        }

        public override float GetSample(float x, float y)
        {
            return 1;
        }

        public override float GetSample(float x, float y, float z)
        {
            return 1;
        }
    }
}