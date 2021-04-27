using UnityEngine;
using XNode;

namespace NoiseUltra.Nodes {
	/// <summary>
	///  Class is able to sample and process a Single Output Node.
	/// </summary>

	public abstract class NodeOutput : NodeBase {
		[SerializeField, Output]
		private NodeBase output;

		public override void OnCreateConnection (NodePort from, NodePort to) {
			Update ();
		}

		private void UpdateOutPut () {
			if (!IsConnected (output)) {
				return;
			}

			var port = GetPort (output);
			int connectionsCount = port.ConnectionCount;
			for (int i = 0; i < connectionsCount; i++) {
				NodePort connectedPort = port.GetConnection (i);
				NodeBase node = connectedPort.node as NodeBase;
				if (node) {
					node.Update ();
				}
			}
		}

		protected override void OnBeforeUpdate () => UpdateOutPut ();
	}
}