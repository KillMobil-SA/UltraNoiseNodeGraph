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
	public class NoiseNodeClamp : NoiseNodeModifierBase
	{
		
		[OnValueChanged("UpdateValues")]
		public Vector2 limit = new Vector2(0, 1);
		[OnValueChanged("UpdateValues")]
		public bool smoothClampValue;
	
		public override float ModifyValue(float v) {
			float newV = Mathf.Clamp(v, limit.x, limit.y);
			if (smoothClampValue) newV = Mathf.InverseLerp(limit.x, limit.y, newV);
			return newV;
		}
		


	}
}