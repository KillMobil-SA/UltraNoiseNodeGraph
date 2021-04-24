using NoiseUltra.Nodes;
using UnityEngine;
using Sirenix.OdinInspector;

namespace NoiseUltra.Operations
{
	public class Clamp : NodeModifier
	{
		[SerializeField] 
		private Vector2 limit = new Vector2(0, 1);
		
		[SerializeField] 
		private bool smoothClampValue;

		protected override float ApplyModifier(float sample)
		{
			float clamp = Mathf.Clamp(sample, limit.x, limit.y);
			if (smoothClampValue)
			{
				clamp = Mathf.InverseLerp(limit.x, limit.y, clamp);
			}
			return clamp;
		}
	}
}