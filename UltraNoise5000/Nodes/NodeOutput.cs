using UnityEngine;
using XNode;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Class processes a Single Output Node.
    /// </summary>
    public abstract class NodeOutput : NodeBase
    {
        [Output]
        [SerializeField]
        private NodeBase output;

        public override void OnCreateConnection(NodePort source, NodePort target)
        {
            if (this != target.node)
            {
                DrawAsync();
            }
        }

        private void UpdateOutPut()
        {
            if (!IsConnected(output))
            {
                return;
            }

            NodePort port = GetPort(output);
            int connectionsCount = port.ConnectionCount;
            for (int i = 0; i < connectionsCount; ++i)
            {
                NodePort connectedPort = port.GetConnection(i);
                NodeBase node = connectedPort.node as NodeBase;
                if (node != null)
                {
                    Debug.Log("UpdateOutPut Draw: " + node.name);
                    node.DrawAsync();
                }
            }
        }

        protected override void OnBeforeDrawPreview()
        {
            UpdateOutPut();
        }
    }
}