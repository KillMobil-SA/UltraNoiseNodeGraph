using NoiseUltra.Nodes;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeProprieties.NodeTintGreen)]
    public class Black : NodeOutput
    {
        public override float GetSample(float x)
        {
            return 0;
        }

        public override float GetSample(float x, float y)
        {
            return 0;
        }

        public override float GetSample(float x, float y, float z)
        {
            return 0;
        }
    }
}