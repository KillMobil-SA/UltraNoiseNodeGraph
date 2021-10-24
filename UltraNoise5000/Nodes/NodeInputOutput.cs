using System;
using UnityEngine;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Samples and processes a Single Input Node into a Single Output Node.
    /// </summary>
    public abstract class NodeInputOutput : NodeOutput
    {
        [Input] 
        [SerializeField]
        private NodeBase input;

        #region Public
        public override float GetSample(float x)
        {
            return IsInputValid ? Mathf.Clamp01(GetInput().GetSample(x)) : NodeProprieties.InvalidValue;
        }

        public override float GetSample(float x, float y)
        {
            return IsInputValid ? Mathf.Clamp01(GetInput().GetSample(x, y)) : NodeProprieties.InvalidValue;
        }

        public override float GetSample(float x, float y, float z)
        {
            return IsInputValid ? Mathf.Clamp01(GetInput().GetSample(x, y, z)) : NodeProprieties.InvalidValue;
        }

        #endregion

        #region Private

        private NodeBase GetInput()
        {
            return GetInputNode(nameof(input), input);
        }

        private bool IsInputValid => GetInput() != null;
        #endregion
    }
}