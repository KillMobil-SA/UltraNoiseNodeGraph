using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NoiseUltra.Generators {
	public class NoiseNodeExternalSource : NoiseNodeBase
	{

		public NoiseNodeBase noiseNodeBase;

		public override float Sample1D (float x) {
			
			return IdentifyBounds (noiseNodeBase.Sample1D(x));
		}

		public override float Sample2D (float x, float y) {
			return IdentifyBounds (noiseNodeBase.Sample2D(x,y));
		}
		public override float Sample3D (float x, float y, float z) {
			return IdentifyBounds (noiseNodeBase.Sample3D(x,y,z));
		}

		[Output] public NoiseNodeBase noiseOutPut;
		public override object GetValue (NodePort port) {
			return this;
		}

	}
}