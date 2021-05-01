using NoiseUltra.Nodes;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeProprieties.NodeTintGreen)]
    public class White : NodeOutput
    {
        public override float Sample1D(float x)
        {
            return 1;
        }

        public override float Sample2D(float x, float y)
        {
            return 1;
        }

        public override float Sample3D(float x, float y, float z)
        {
            return 1;
        }
    }
}