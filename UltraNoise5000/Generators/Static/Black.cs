using NoiseUltra.Nodes;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeProprieties.NodeTintGreen)]
    public class Black : NodeOutput
    {
        public override float Sample1D(float x)
        {
            return 0;
        }

        public override float Sample2D(float x, float y)
        {
            return 0;
        }

        public override float Sample3D(float x, float y, float z)
        {
            return 0;
        }
    }
}