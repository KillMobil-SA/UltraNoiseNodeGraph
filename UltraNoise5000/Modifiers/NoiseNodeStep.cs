using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralNoiseProject;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using XNode;

namespace NoiseUltra.Modifiers
{
	public class NoiseNodeStep : NoiseNodeModifierBase
	{
		
		[OnValueChanged("UpdateValues")]
		[Range (0, 10 )]
		public int steps = 0;
	
		public override float ModifyValue(float v) {
			if (steps != 0)
				return Mathf.Floor(steps * v) / steps;
			else
				return v;
		}


	}
}