using System;

namespace XNode.Examples.LogicToy
{
    /// <summary> Base node for the LogicToy system </summary>
    public abstract class LogicNode : Node
    {
        public Action onStateChange;
        public abstract bool led { get; }

        public void SendSignal(NodePort output)
        {
            // Loop through port connections
            var connectionCount = output.ConnectionCount;
            for (var i = 0; i < connectionCount; i++)
            {
                var connectedPort = output.GetConnection(i);

                // Get connected ports logic node
                var connectedNode = connectedPort.node as LogicNode;

                // Trigger it
                if (connectedNode != null) connectedNode.OnInputChanged();
            }

            if (onStateChange != null) onStateChange();
        }

        protected abstract void OnInputChanged();
    }
}