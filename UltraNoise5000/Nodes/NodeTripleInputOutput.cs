using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Class samples and processes three Input Nodes into a Single Output Node.
    /// </summary>
    public abstract class NodeTripleInputOutput : NodeOutput
    {
        [SerializeField] 
        [Range(NodeProprieties.MIN_VALUE, NodeProprieties.MAX_VALUE)] 
        [OnValueChanged(nameof(DrawAsync))]
        private float strengthA = NodeProprieties.MAX_VALUE;

        [SerializeField] 
        [Range(NodeProprieties.MIN_VALUE, NodeProprieties.MAX_VALUE)] 
        [OnValueChanged(nameof(DrawAsync))]
        private float strengthB = NodeProprieties.MAX_VALUE;

        [SerializeField] 
        [Range(NodeProprieties.MIN_VALUE, NodeProprieties.MAX_VALUE)] 
        [OnValueChanged(nameof(DrawAsync))]
        private float strengthC = NodeProprieties.MAX_VALUE;

        [Input] 
        [SerializeField] 
        private NodeBase inputA;

        [Input] 
        [SerializeField]
        private NodeBase inputB;

        [Input]
        [SerializeField]
        private NodeBase inputC;

        #region Public
        public override float GetSample(float x)
        {
            if (!IsValid)
                return NodeProprieties.INVALID_VALUE;

            var sampleA = GetInputA().GetSample(x);
            var sampleB = GetInputB().GetSample(x);
            var sampleC = GetInputC().GetSample(x);
            return Clamp(sampleA * strengthA, sampleB * strengthB, sampleC * strengthC);
        }

        public override float GetSample(float x, float y)
        {
            if (!IsValid)
                return NodeProprieties.INVALID_VALUE;

            var sampleA = GetInputA().GetSample(x, y);
            var sampleB = GetInputB().GetSample(x, y);
            var sampleC = GetInputC().GetSample(x, y);
            return Clamp(sampleA * strengthA, sampleB * strengthB, sampleC * strengthC);
        }

        public override float GetSample(float x, float y, float z)
        {
            if (!IsValid)
                return NodeProprieties.INVALID_VALUE;

            var sampleA = GetInputA().GetSample(x, y, z);
            var sampleB = GetInputB().GetSample(x, y, z);
            var sampleC = GetInputC().GetSample(x, y, z);
            return Clamp(sampleA * strengthA, sampleB * strengthB, sampleC * strengthC);
        }
        #endregion

        #region Private
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

        private float Clamp(float sampleA, float sampleB, float sampleC)
        {
            return Mathf.Clamp01(ExecuteOperation(sampleA, sampleB, sampleC));
        }
        #endregion

        protected abstract float ExecuteOperation(float strengthenedA, float strengthenedB, float strengthenedC);
    }
}