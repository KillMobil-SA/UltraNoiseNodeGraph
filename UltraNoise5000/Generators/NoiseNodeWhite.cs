using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators {
	public class NoiseNodeWhite : NoiseNodeBase {


		public override float Sample1D (float x) {
			float v = 1;
			return v;
		}

		public override float Sample2D (float x, float y) {
			float v = 1;
			return v;
		}
		public override float Sample3D (float x, float y, float z) {
			float v = 1;
			return v;
		}

		[Output] public NoiseNodeBase noiseOutPut;
		public override object GetValue (NodePort port) {
			return this;
		}

	}
}