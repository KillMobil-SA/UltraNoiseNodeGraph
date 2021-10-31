using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Samples and processes Two Input Nodes into a single Output Node and multiplies by a strength factor.
    /// </summary>
    public abstract class NodeDoubleInputOutput : NodeOutput
    {
        #region Members
        [SerializeField]
        [Range(NodeProprieties.MIN_VALUE, NodeProprieties.MAX_VALUE)]
        [OnValueChanged(nameof(DrawAsync))]
        private float strengthA = NodeProprieties.MAX_VALUE;

        [SerializeField]
        [Range(NodeProprieties.MIN_VALUE, NodeProprieties.MAX_VALUE)]
        [OnValueChanged(nameof(DrawAsync))]
        private float strengthB = NodeProprieties.MAX_VALUE;

        [SerializeField]
        [Input] 
        private NodeBase inputA;

        [SerializeField]
        [Input] 
        private NodeBase inputB;
        #endregion

        private bool IsValid => GetInputA() != null &&
                                GetInputB() != null;

        #region Public
        public override float GetSample(float x)
        {
            if (!IsValid)
            {
                return -1;
            }

            float sampleA = GetInputA().GetSample(x);
            float sampleB = GetInputB().GetSample(x);
            return Clamp(sampleA * strengthA, sampleB * strengthB);
        }

        public override float GetSample(float x, float y)
        {
            if (!IsValid)
            {
                return -1;
            }

            float sampleA = GetInputA().GetSample(x, y);
            float sampleB = GetInputB().GetSample(x, y);
            return Clamp(sampleA * strengthA, sampleB * strengthB);
        }

        public override float GetSample(float x, float y, float z)
        {
            if (!IsValid)
            {
                return -1;
            }

            float sampleA = GetInputA().GetSample(x, y, z);
            float sampleB = GetInputB().GetSample(x, y, z);
            return Clamp(sampleA * strengthA, sampleB * strengthB);
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