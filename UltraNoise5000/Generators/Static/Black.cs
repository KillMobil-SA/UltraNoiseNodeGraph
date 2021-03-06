using System;
using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeColor.GENERATOR_OTHER)]
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