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
	public class NoiseNodeOneMinus : NoiseNodeModifierBase
	{
		
		public override float ModifyValue(float v) {
			return 1 - v;
		}
		
		
	}
}