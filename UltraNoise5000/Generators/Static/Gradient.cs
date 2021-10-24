using System;
using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Generators.Static
{
    [NodeTint(NodeColor.Green)]
    public class Gradient : NodeOutput
    {
        [SerializeField] private GrandientLineType lineType;
        [SerializeField] private float start;
        [SerializeField] private float end = 256;

        public override float GetSample(float x)
        {
            return Mathf.Lerp(start, end, x);
        }

        public override float GetSample(float x, float y)
        {
            return Mathf.InverseLerp(start, end, lineType == GrandientLineType.Horizontal ? x : y);
        }

        public override float GetSample(float x, float y, float z)
        {
            return Mathf.InverseLerp(start, end, lineType == GrandientLineType.Horizontal ? x : y);
        }

        private enum GrandientLineType
        {
            Horizontal,
            Vertical
        }
    }
}