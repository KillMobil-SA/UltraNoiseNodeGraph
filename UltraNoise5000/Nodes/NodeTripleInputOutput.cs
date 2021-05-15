using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Class is able to sample and process a Three Input Nodes and a Single Output Node.
    /// </summary>
    public abstract class NodeTripleInputOutput : NodeOutput
    {
        private const float MinStrength = 0;
        private const float MaxStrength = 1; // I think the max go be bigger than 1 here, no?

        [SerializeField] [Range(MinStrength, MaxStrength)] [OnValueChanged(nameof(Draw))]
        private float strengthA = MaxStrength;

        [SerializeField] [Range(MinStrength, MaxStrength)] [OnValueChanged(nameof(Draw))]
        private float strengthB = MaxStrength;

        [SerializeField] [Range(MinStrength, MaxStrength)] [OnValueChanged(nameof(Draw))]
        private float strengthC = MaxStrength;

        [SerializeField] [Input] private NodeBase inputA;

        [SerializeField] [Input] private NodeBase inputB;

        [SerializeField] [Input] private NodeBase inputC;

        private bool IsValid => GetInputA() != null &&
                                GetInputB() != null &&
                                GetInputC() != null;

        private NodeBase GetInputA()
        {
            return GetInputNode(nameof(inputA), inputA);
        }

        private NodeBase GetInputB()
        {
            return GetInputNode(nameof(inputB), inputB);
        }

        private NodeBase GetInputC()
        {
            return GetInputNode(nameof(inputC), inputC);
        }

        public override float GetSample(float x)
        {
            if (!IsValid)
                return NodeProprieties.Invalid;

            var sampleA = GetInputA().GetSample(x);
            var sampleB = GetInputB().GetSample(x);
            var sampleC = GetInputC().GetSample(x);
            return Clamp(sampleA * strengthA, sampleB * strengthB, sampleC * strengthC);
        }

        public override float GetSample(float x, float y)
        {
            if (!IsValid)
                return NodeProprieties.Invalid;

            var sampleA = GetInputA().GetSample(x, y);
            var sampleB = GetInputB().GetSample(x, y);
            var sampleC = GetInputC().GetSample(x, y);
            return Clamp(sampleA * strengthA, sampleB * strengthB, sampleC * strengthC);
        }

        public override float GetSample(float x, float y, float z)
        {
            if (!IsValid)
                return NodeProprieties.Invalid;

            var sampleA = GetInputA().GetSample(x, y, z);
            var sampleB = GetInputB().GetSample(x, y, z);
            var sampleC = GetInputC().GetSample(x, y, z);
            return Clamp(sampleA * strengthA, sampleB * strengthB, sampleC * strengthC);
        }

        private float Clamp(float sampleA, float sampleB, float sampleC)
        {
            return Mathf.Clamp01(ExecuteOperation(sampleA, sampleB, sampleC));
        }

        /// <summary>
        ///     Gets the final operation from the sub class.
        /// </summary>
        protected abstract float ExecuteOperation(float strengthenedA, float strengthenedB, float strengthenedC);
    }
}