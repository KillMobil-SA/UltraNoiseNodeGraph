using System;
using UnityEngine;

namespace XNode.Examples.StateGraph
{
    public class StateNode : Node
    {
        [Input] public Empty enter;
        [Output] public Empty exit;

        public void MoveNext()
        {
            var fmGraph = graph as StateGraph;

            if (fmGraph.current != this)
            {
                Debug.LogWarning("Node isn't active");
                return;
            }

            var exitPort = GetOutputPort("exit");

            if (!exitPort.IsConnected)
            {
                Debug.LogWarning("Node isn't connected");
                return;
            }

            var node = exitPort.Connection.node as StateNode;
            node.OnEnter();
        }

        public void OnEnter()
        {
            var fmGraph = graph as StateGraph;
            fmGraph.current = this;
        }

        [Serializable]
        public class Empty
        {
        }
    }
}