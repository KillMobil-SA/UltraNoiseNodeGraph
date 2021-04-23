using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra
{
	[NodeWidth(NodeProprieties.NodeWidth)]
	public class NoiseNodeBase : Node
	{
		[SerializeField] 
		private PreviewImage previewImage;
		
		[Button]
		public virtual void Update()
		{
			UpdatePreview();
		}

		protected void UpdatePreview()
		{
			previewImage.Update(Sample2D);
		}
		
		protected override void Init()
		{
			previewImage = new PreviewImage();
		}
		
		public override void OnCreateConnection(NodePort from, NodePort to)
		{
			Update();
		}

		public virtual float Sample1D(float x)
		{
			return -1;
		}

		public virtual float Sample2D(float x, float y)
		{
			return -1;
		}

		public virtual float Sample3D(float x, float y, float z)
		{
			return -1;
		}
	}
}