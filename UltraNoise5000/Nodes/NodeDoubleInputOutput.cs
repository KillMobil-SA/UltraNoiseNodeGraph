using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Class is able to sample and process a Two Input Nodes and a Single Output Node.
    /// </summary>
    public abstract class NodeDoubleInputOutput : NodeOutput
    {
        private const float MinStrength = 0;
        private const float MaxStrength = 1; // I think the max go be bigger than 1 here, no?

        [SerializeField] [Range(MinStrength, MaxStrength)] [OnValueChanged(nameof(Draw))]
        private float strengthA = MaxStrength;

        [SerializeField] [Range(MinStrength, MaxStrength)] [OnValueChanged(nameof(Draw))]
        private float strengthB = MaxStrength;

        [SerializeField] [Input] private NodeBase inputA;

        [SerializeField] [Input] private NodeBase inputB;

        private bool IsValid => GetInputA() != null &&
                                GetInputB() != null;

        private NodeBase GetInputA()
        {
            return GetInputNode(nameof(inputA), inputA);
        }

        private NodeBase GetInputB()
        {
            return GetInputNode(nameof(inputB), inputB);
        }

        public override float GetSample(float x)
        {
            if (!IsValid)
                return -1;

            var sampleA = GetInputA().GetSample(x);
            var sampleB = GetInputB().GetSample(x);
            return ExecuteOperation(sampleA * strengthA, sampleB * strengthB);
        }

        public override float GetSample(float x, float y)
        {
            if (!IsValid)
                return -1;

            var sampleA = GetInputA().GetSample(x, y);
            var sampleB = GetInputB().GetSample(x, y);
            return ExecuteOperation(sampleA * strengthA, sampleB * strengthB);
        }

        public override float GetSample(float x, float y, float z)
        {
            if (!IsValid)
                return -1;

            var sampleA = GetInputA().GetSample(x, y, z);
            var sampleB = GetInputB().GetSample(x, y, z);
            return ExecuteOperation(sampleA * strengthA, sampleB * strengthB);
        }

        /// <summary>
        ///     Gets the final operation from the sub class.
        /// </summary>
        protected abstract float ExecuteOperation(float strengthenedA, float strengthenedB);
    }
}