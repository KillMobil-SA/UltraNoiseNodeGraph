﻿using NoiseUltra.Nodes;
using UnityEngine;

namespace NoiseUltra.Operations
{
	public class Curve : NodeModifier
	{
		[SerializeField]
		private AnimationCurve resultCurve = AnimationCurve.Linear(0, 0, 1, 1);

		protected override float ApplyModifier(float sample)
		{
			return resultCurve.Evaluate(sample);
		}
	}
}