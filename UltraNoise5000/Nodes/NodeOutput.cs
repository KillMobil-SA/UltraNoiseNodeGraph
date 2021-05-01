using UnityEngine;
using XNode;

namespace NoiseUltra.Nodes
{
    /// <summary>
    ///     Class is able to sample and process a Single Output Node.
    /// </summary>
    public abstract class NodeOutput : NodeBase
    {
        [SerializeField] [Output] private NodeBase output;

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            Update();
        }

        private void UpdateOutPut()
        {
            if (!IsConnected(output)) return;

            var port = GetPort(output);
            var connectionsCount = port.ConnectionCount;
            for (var i = 0; i < connectionsCount; i++)
            {
                var connectedPort = port.GetConnection(i);
                var node = connectedPort.node as NodeBase;
                if (node) node.Update();
            }
        }

        protected override void OnBeforeUpdate()
        {
            UpdateOutPut();
        }
    }
}