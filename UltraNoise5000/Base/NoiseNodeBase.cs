using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra
{
	[NodeWidth(NodeProprieties.NodeWidth)]
	public class NoiseNodeBase : Node
	{
		
			
		[Button , HideIf("hasPreview" ) , ]
		protected void EnablePreview()
		{
			hasPreview = true;
			previewImage = new PreviewImage();
			Update();
		}

		[Button , ShowIf("hasPreview")]
		protected void DisaablePreview()
		{
			hasPreview = false;
			previewImage.DeleteTexture();
			previewImage = null;
		}
		
		[SerializeField , ShowIf("hasPreview")] 
		private PreviewImage previewImage;
		private bool hasPreview = false;
		
		
		[Button]
		public virtual void Update()
		{
			UpdatePreview();
		}

		protected void UpdatePreview()
		{
			if(hasPreview)
				previewImage.Update(Sample2D);
			//else
				UpdateOutPut();
			

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
		
		
		public void UpdateOutPut()
		{
			
			NodePort noiseOutPutPort = GetOutputPort("noiseOutPut");
			if (!noiseOutPutPort.IsConnected) {
				return;
			}

			int connectionsCount = noiseOutPutPort.ConnectionCount;
			for (int i = 0; i < connectionsCount; i++)
			{
				NodePort connectedPort = noiseOutPutPort.GetConnection(i);
				NoiseNodeBase node = connectedPort.node as NoiseNodeBase;
				node.Update();
			}
		}

		
		[Output] public NoiseNodeBase noiseOutPut;
		public override object GetValue(NodePort port)
		{
			return this; 
		}

	}
}