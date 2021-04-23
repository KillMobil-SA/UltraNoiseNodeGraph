﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using XNode;

namespace NoiseUltra.Operation
{
	public class NoiseNodeSub : NoiseNodeOperatorBase
	{
		
		[Range(0,1)]
		[OnValueChanged("UpdateValues")]
		public float subStrength;
		
		public override float Sample1D (float x)
		{
			if (noiseAInput == null || noiseBInput == null) return -1;
			float v = noiseAInput.Sample1D (x);
			v -= noiseBInput.Sample1D(x) * subStrength;
			return v;
		}

		public override float Sample2D (float x , float y) {
			if (noiseAInput == null || noiseBInput == null) return -1;
			float v = noiseAInput.Sample2D (x , y);
			v -= noiseBInput.Sample2D(x , y) * subStrength;
			return v;
		}

		public override float Sample3D (float x , float y , float z) {
			if (noiseAInput == null || noiseBInput == null) return -1;
			float v = noiseAInput.Sample3D (x , y , z);
			v -= noiseBInput.Sample3D(x , y , z) * subStrength;
			return v;
		}


	



		
		
	}
}