using Sirenix.OdinInspector;
using UnityEngine;
namespace NoiseUltra.Nodes {
    /// <summary>
    ///  Class is able to sample and process a Three Input Nodes and a Single Output Node.
    /// </summary>
    public abstract class NodeTripleInputOutput : NodeOutput {
        private const float MinStrength = 0;
        private const float MaxStrength = 1; // I think the max go be bigger than 1 here, no?

        [SerializeField, Range (MinStrength, MaxStrength), OnValueChanged (nameof (Update))]
        private float strengthA = MaxStrength;

        [SerializeField, Range (MinStrength, MaxStrength), OnValueChanged (nameof (Update))]
        private float strengthB = MaxStrength;

        [SerializeField, Range (MinStrength, MaxStrength), OnValueChanged (nameof (Update))]
        private float strengthC = MaxStrength;

        [SerializeField, Input]
        private NodeBase inputA;

        [SerializeField, Input]
        private NodeBase inputB;

        [SerializeField, Input]
        private NodeBase inputC;

        private bool IsValid => GetInputA () != null &&
            GetInputB () != null &&
            GetInputC () != null;

        private NodeBase GetInputA () {
            return GetInputNode (nameof (inputA), inputA);
        }

        private NodeBase GetInputB () {
            return GetInputNode (nameof (inputB), inputB);
        }

        private NodeBase GetInputC () {
            return GetInputNode (nameof (inputC), inputC);
        }

        public override float Sample1D (float x) {
            if (!IsValid)
                return -1;

            var sampleA = GetInputA ().Sample1D (x);
            var sampleB = GetInputB ().Sample1D (x);
            var sampleC = GetInputC ().Sample1D (x);
            return ExecuteOperation (sampleA * strengthA, sampleB * strengthB, sampleC * strengthC);
        }

        public override float Sample2D (float x, float y) {
            if (!IsValid)
                return -1;

            var sampleA = GetInputA ().Sample2D (x, y);
            var sampleB = GetInputB ().Sample2D (x, y);
            var sampleC = GetInputC ().Sample2D (x, y);
            return ExecuteOperation (sampleA * strengthA, sampleB * strengthB, sampleC * strengthC);
        }

        public override float Sample3D (float x, float y, float z) {
            if (!IsValid)
                return -1;

            var sampleA = GetInputA ().Sample3D (x, y, z);
            var sampleB = GetInputB ().Sample3D (x, y, z);
            var sampleC = GetInputC ().Sample3D (x, y, z);
            return ExecuteOperation (sampleA * strengthA, sampleB * strengthB, sampleC * strengthC);
        }

        /// <summary>
        ///     Gets the final operation from the sub class.
        /// </summary>
        protected abstract float ExecuteOperation (float strengthenedA, float strengthenedB, float strengthenedC);
    }
}