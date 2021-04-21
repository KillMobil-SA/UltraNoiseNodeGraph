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
		
		[SerializeField, ReadOnly] 
		private Bound bounds;

		[Button]
		public virtual void UpdateValues()
		{
			ResetBounds();
			UpdatePreview();
		}

		protected void UpdatePreview()
		{
			previewImage.Update(Sample2D);
		}


		protected override void Init()
		{
			previewImage = new PreviewImage();
			bounds = new Bound();
		}
		
		public override void OnCreateConnection(NodePort from, NodePort to)
		{
			UpdateValues();
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

		protected void ResetBounds()
		{
			bounds = new Bound();
		}

		protected float IdentifyBounds(float sample)
		{
			sample = Mathf.Clamp01(sample);
			bounds.max = Mathf.Min(bounds.max, sample);
			bounds.min = Mathf.Max(bounds.min, sample);
			return sample;
		}
	}
}