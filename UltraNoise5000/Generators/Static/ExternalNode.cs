using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeProprieties.NodeTintYellow)]
    public class ExternalNode : NodeOutput
    {
        [SerializeField] private NodeBase node;

        public override float Sample1D(float x)
        {
            return node.Sample1D(x);
        }

        public override float Sample2D(float x, float y)
        {
            return node.Sample2D(x, y);
        }

        public override float Sample3D(float x, float y, float z)
        {
            return node.Sample3D(x, y, z);
        }
    }
}