﻿using UnityEngine;
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
            Draw();
        }

        private void UpdateOutPut()
        {
            if (!IsConnected(output))
            {
                return;
            }

            var port = GetPort(output);
            var connectionsCount = port.ConnectionCount;
            for (var i = 0; i < connectionsCount; i++)
            {
                var connectedPort = port.GetConnection(i);
                var node = connectedPort.node as NodeBase;
                if (node)
                {
                    node.Draw();
                }
            }
        }

        protected override void OnBeforeDrawPreview()
        {
            UpdateOutPut();
        }
    }
}