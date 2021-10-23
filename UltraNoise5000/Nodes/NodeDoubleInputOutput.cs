using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Samples and processes Two Input Nodes into a single Output Node.
    /// </summary>
    public abstract class NodeDoubleInputOutput : NodeOutput
    {
        #region Members
        private const float MinStrength = 0;
        private const float MaxStrength = 1;

        [SerializeField] [Range(MinStrength, MaxStrength)] [OnValueChanged(nameof(Draw))]
        private float strengthA = MaxStrength;

        [SerializeField] [Range(MinStrength, MaxStrength)] [OnValueChanged(nameof(Draw))]
        private float strengthB = MaxStrength;

        [SerializeField] [Input] 
        private NodeBase inputA;

        [SerializeField] [Input] 
        private NodeBase inputB;
        #endregion

        private bool IsValid => GetInputA() != null &&
                                GetInputB() != null;

        #region Public
        public override float GetSample(float x)
        {
            if (!IsValid)
                return -1;

            var sampleA = GetInputA().GetSample(x);
            var sampleB = GetInputB().GetSample(x);
            return Clamp(sampleA * strengthA, sampleB * strengthB);
        }

        public override float GetSample(float x, float y)
        {
            if (!IsValid)
                return -1;

            var sampleA = GetInputA().GetSample(x, y);
            var sampleB = GetInputB().GetSample(x, y);
            return Clamp(sampleA * strengthA, sampleB * strengthB);
        }

        public override float GetSample(float x, float y, float z)
        {
            if (!IsValid)
                return -1;

            var sampleA = GetInputA().GetSample(x, y, z);
            var sampleB = GetInputB().GetSample(x, y, z);
            return Clamp(sampleA * strengthA, sampleB * strengthB);
        }

        public override void GetSampleAsync(float x, float y, int index, ref Color[] colors, Action onComplete)
        {

        }
        #endregion

        #region Private
        private NodeBase GetInputA()
        {
            return GetInputNode(nameof(inputA), inputA);
        }

        private NodeBase GetInputB()
        {
            return GetInputNode(nameof(inputB), inputB);
        }

        private float Clamp(float sampleA, float sampleB)
        {
            return Mathf.Clamp01(ExecuteOperation(sampleA, sampleB));
        }
        #endregion

        protected abstract float ExecuteOperation(float strengthenedA, float strengthenedB);
    }
}