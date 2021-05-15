using UnityEngine;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Class is able to sample and process a Single Input Node and a Single Output Node.
    /// </summary>
    public abstract class NodeInputOutput : NodeOutput
    {
        [SerializeField] [Input] private NodeBase input;

        private bool IsValid => GetInput() != null;

        public override float GetSample(float x)
        {
            if (!IsValid)
                return NodeProprieties.Invalid;

            return Clamp(GetInput().GetSample(x));
        }

        public override float GetSample(float x, float y)
        {
            if (!IsValid)
                return NodeProprieties.Invalid;

            return Clamp(GetInput().GetSample(x, y));
        }

        public override float GetSample(float x, float y, float z)
        {
            if (!IsValid)
                return NodeProprieties.Invalid;

            return Clamp(GetInput().GetSample(x, y, z));
        }
        
        private float Clamp(float sample)
        {
            return Mathf.Clamp01(sample);
        }

        private NodeBase GetInput()
        {
            return GetInputNode(nameof(input), input);
        }
    }
}