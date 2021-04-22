using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using XNode;

namespace NoiseUltra {
	[CreateAssetMenu (fileName = "Ultra Noise Graph", menuName = "KillMobil/UltraNoise/Noise Graph")]
	public class NoiseNodeGraph : NodeGraph {


		[Button]
		public void UpdateAllValues()
		{
			foreach (Node n in nodes)
			{
				if (n is NoiseNodeBase) {
					NoiseNodeBase nnb = n as NoiseNodeBase;
					nnb.Update(); 
				}
			}
		}


	}
}