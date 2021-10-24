using System;
using NoiseUltra.Nodes;
using NoiseUltra.Output;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeColor.Yellow)]
    public class ExternalNode : NodeOutput
    {
        [SerializeField] private ExportNode node;

        private bool IsValid => node != null;
        
        public override float GetSample(float x)
        {
            if (!IsValid) return 0;
            return node.GetSample(x);
        }

        public override float GetSample(float x, float y)
        {
            if (!IsValid) return 0;
            return node.GetSample(x, y);
        }

        public override float GetSample(float x, float y, float z)
        {
            if (!IsValid) return 0;
            return node.GetSample(x, y, z);
        }
    }
}