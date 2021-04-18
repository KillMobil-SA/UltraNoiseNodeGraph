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
	public class NoiseNodeLerp : NoiseNodeOperatorBase
	{
		
		[Range(0,1)]
		[OnValueChanged("UpdateValues")]
		public float perc;
		
		public override float Sample1D (float x)
		{
			if (noiseAInput == null || noiseBInput == null) return -1;
			float v = Mathf.Lerp(noiseAInput.Sample1D(x), noiseBInput.Sample1D(x), perc);

			return DebugValues (v);
		}

		public override float Sample2D (float x , float y) {
			if (noiseAInput == null || noiseBInput == null) return -1;
			float v = Mathf.Lerp(noiseAInput.Sample2D(x , y), noiseBInput.Sample2D(x, y), perc);
			return DebugValues (v);
		}

		public override float Sample3D (float x , float y , float z) {
			if (noiseAInput == null || noiseBInput == null) return -1;
			float v = Mathf.Lerp(noiseAInput.Sample3D(x , y , z), noiseBInput.Sample3D(x , y , z), perc);
			return DebugValues (v);
		}
		
		
	}
}