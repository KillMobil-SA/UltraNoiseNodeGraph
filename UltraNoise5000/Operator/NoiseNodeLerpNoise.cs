using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using XNode;

namespace NoiseUltra.Operation
{
	public class NoiseNodeLerpNoise : NoiseNodeOperatorBase
	{

		[Input] public NoiseNodeBase noiseLerp;

	
		public override float Sample1D (float x)
		{
			if (noiseAInput == null || noiseBInput == null || noiseLerpInput == null) return -1;
			float v = Mathf.Lerp(noiseAInput.Sample1D(x), noiseBInput.Sample1D(x), noiseLerpInput.Sample1D(x));

			return IdentifyBounds (v);
		}

		public override float Sample2D (float x , float y) {
			if (noiseAInput == null || noiseBInput == null || noiseLerpInput == null) return -1;
			float v = Mathf.Lerp(noiseAInput.Sample2D(x , y), noiseBInput.Sample2D(x, y), noiseLerpInput.Sample2D(x, y));
			return IdentifyBounds (v);
		}

		public override float Sample3D (float x , float y , float z) {
			if (noiseAInput == null || noiseBInput == null || noiseLerpInput == null) return -1;
			float v = Mathf.Lerp(noiseAInput.Sample3D(x , y , z), noiseBInput.Sample3D(x , y , z), noiseLerpInput.Sample3D(x, y , z));
			return IdentifyBounds (v);
		}


	

	
		public override void OnCreateConnection(NodePort from, NodePort to) {
			base.OnCreateConnection(from , to);
			_noiseLerpInput = null;
			UpdateValues();
		}


	
		private NoiseNodeBase _noiseLerpInput;
		private NoiseNodeBase noiseLerpInput
		{
			get {
				if (_noiseLerpInput == null)
					_noiseLerpInput = GetInputValue<NoiseNodeBase>("noiseLerp", this.noiseLerp);
				return _noiseLerpInput;
			}
			set { _noiseLerpInput = value; }
		}

		
	}
}