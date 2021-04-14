using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra {
	[NodeWidth (286)]
	public class NoiseNodeBase : Node {
		public int seed;
		[ReadOnly]
		public Vector2 minMaxNoiseValue = new Vector2 (float.NegativeInfinity, float.PositiveInfinity);

		[Title ("Noise Preview")]
		Rect rect = new Rect (0, 0, 256, 256);
		[OnValueChanged ("UpdatePreview")]
		public float zoomAmount = 200;
		[PreviewField (256)]
		public Texture2D sourceTexture;

		public virtual void UpdatePreview () {
			sourceTexture = null;
			sourceTexture = new Texture2D (Mathf.RoundToInt (rect.width), Mathf.RoundToInt (rect.height), TextureFormat.RGB24, false, false);
			for (int x = 0; x < rect.width; x++) {
				for (int y = 0; y < rect.height; y++) {
					float fx = x / (rect.width - 1.0f);
					float fy = y / (rect.height - 1.0f);
					float v = Sample2D (fx * zoomAmount, fy * zoomAmount);
					sourceTexture.SetPixel (x, y, new Color (v, v, v, 1));

				}
			}
			sourceTexture.Apply ();
		}

		public override void OnCreateConnection (NodePort from, NodePort to) {
			UpdateValues ();
		}

		
		[Button]
		public virtual void UpdateValues () {
			ResetDebugValues ();
			UpdatePreview ();
		}


		protected override void Init()
		{
			NoiseInit ();
		}
		
		public virtual void NoiseInit()
		{
			//UpdateValues();
		}

		public virtual float Sample1D (float x) {
			return -1;
		}

		public virtual float Sample2D (float x, float y) {
			return -1;
		}

		public virtual float Sample3D (float x, float y, float z) {
			return -1;
		}

		//Noise Cache
		
		
		
		
		#region DebugValues

		public void ResetDebugValues () {
			minMaxNoiseValue = new Vector2 (float.PositiveInfinity, float.NegativeInfinity);
		}

		public float DebugValues (float v) {
			v = Mathf.Clamp01 (v);
			minMaxNoiseValue.x = Mathf.Min (minMaxNoiseValue.x, v);
			minMaxNoiseValue.y = Mathf.Max (minMaxNoiseValue.y, v);
			return v;
		}

		#endregion

	}
}