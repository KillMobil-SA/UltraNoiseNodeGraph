using System;
using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeColor.Green)]
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

        public override void GetSampleAsync(float x, float y, int index, ref Color[] colors, Action onComplete)
        {

        }
    }
}