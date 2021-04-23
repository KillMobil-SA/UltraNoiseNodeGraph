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
	public class NoiseNodeCurve : NoiseNodeModifierBase
	{
		
		[OnValueChanged("Update")]
		public AnimationCurve resultCurve = AnimationCurve.Linear(0, 0, 1, 1);

		
		public override float ModifyValue(float v) {
			return v = resultCurve.Evaluate(v);
		}
		

		/*
		[Button]
		public override void Update () {
			Debug.Log(string.Format("NoiseNodeCurve - UpdateValues ()"));
			ResetConnectionReferences();
			UpdatePreview ();
		}
		*/

	}
}