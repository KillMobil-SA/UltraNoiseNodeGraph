using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeProprieties.NodeTintYellow)]
    public class ExternalNode : NodeOutput
    {
        [SerializeField] private NodeBase node;

        public override float GetSample(float x)
        {
            return node.GetSample(x);
        }

        public override float GetSample(float x, float y)
        {
            return node.GetSample(x, y);
        }

        public override float GetSample(float x, float y, float z)
        {
            return node.GetSample(x, y, z);
        }
    }
}