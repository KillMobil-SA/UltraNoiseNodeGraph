using System.Collections.Generic;
using UnityEngine;
using XNode;

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
            return IsInputValid ? Mathf.Clamp01(GetInput().GetSample(x)) : NodeProprieties.INVALID_VALUE;
        }

        public override float GetSample(float x, float y)
        {
            return IsInputValid ? Mathf.Clamp01(GetInput().GetSample(x, y)) : NodeProprieties.INVALID_VALUE;
        }

        public override float GetSample(float x, float y, float z)
        {
            return IsInputValid ? Mathf.Clamp01(GetInput().GetSample(x, y, z)) : NodeProprieties.INVALID_VALUE;
        }

        protected bool isInputConnected(NodeBase node)
        {
            NodePort inputPort = GetPort("input");
            List<NodePort> connectionList = inputPort.GetConnections();
            for (int i = 0; i < connectionList.Count; i++)
                if (node == connectionList[i].node) return true;
            
            return false;
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