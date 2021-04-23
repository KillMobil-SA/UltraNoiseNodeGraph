using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators {
	public class NoiseNodeTexture : NoiseNodeBase {

		[OnValueChanged ("UpdateTextureRect")]
		public Texture2D texture2d;
		public float scale = 1;
		public Vector2 offSet = new Vector2 (0, 0);
		public Rect textureRect;

		public void UpdateTextureRect () {
			Debug.Log (string.Format ("UpdateTextureRect {0}", texture2d.width));
			textureRect = new Rect (0, 0, texture2d.width, texture2d.height);
			UpdatePreview ();
		}

		protected override void Init () {
			base.Init ();
			UpdatePreview ();
		}


		public override float Sample1D (float x) {
			float v = 0;
			return v;
		}

		public override float Sample2D (float x, float y) {
			float v = 0;

			Vector2 posCord = new Vector2 (x, y);
			Vector2 posCordAltered = (posCord + offSet) * scale;

			if (texture2d != null && textureRect.Contains (posCordAltered)) {
				v = texture2d.GetPixel (Mathf.RoundToInt (posCordAltered.x), Mathf.RoundToInt (posCordAltered.y)).r;
			}

			return v;
		}
		public override float Sample3D (float x, float y, float z) {
			float v = 0;
			if (texture2d != null && textureRect.Contains (new Vector2 (x, z))) {
				v = texture2d.GetPixel (Mathf.RoundToInt (x), Mathf.RoundToInt (z)).r;
			}

			return v;
		}

	

	}
}