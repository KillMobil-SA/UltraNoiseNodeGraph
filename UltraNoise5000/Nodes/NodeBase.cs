using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Nodes
{
	/// <summary>
	///  Class handles the image preview and basic node operations.
	/// </summary>
	[NodeWidth(NodeProprieties.NodeWidth)]
	public abstract class NodeBase : Node
	{
		[SerializeField, Output] 
		private NodeBase output;

		[SerializeField, ShowIf(nameof(_isPreviewEnabled))]
		private PreviewImage previewImage;
		
		private bool _isPreviewEnabled = true;
		
		protected override void Init()
		{
			previewImage = new PreviewImage();
			if(_isPreviewEnabled)
				EnablePreview();
		}
		
		[Button, HideIf(nameof(_isPreviewEnabled))]
		protected void EnablePreview()
		{
			_isPreviewEnabled = true;
			previewImage = new PreviewImage();
			Update();
		}

		[Button, ShowIf(nameof(_isPreviewEnabled))]
		protected void DisablePreview()
		{
			_isPreviewEnabled = false;
			previewImage.DeleteTexture();
			previewImage = null;
		}

		[Button]
		public virtual void Update()
		{
			OnUpdate();
			if (_isPreviewEnabled)
			{
				previewImage.Update(Sample2D);
			}
		}

		protected abstract void OnUpdate();
		public abstract float Sample1D(float x);
		public abstract float Sample2D(float x, float y);
		public abstract float Sample3D(float x, float y, float z);

		public override object GetValue(NodePort port)
		{
			return this;
		}

		protected bool IsConnected(NodeBase node)
		{
			var port = GetPort(nameof(node));
			return port != null && port.IsConnected;
		}

		protected NodePort GetPort(NodeBase node)
		{
			return GetPort(nameof(node));
		}
		
		protected NodeBase GetInputNode(string nodeName, NodeBase fallback)
		{
			return GetInputValue(nodeName, fallback);
		}
	}
}