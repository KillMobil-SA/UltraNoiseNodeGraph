using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using XNode;

namespace NoiseUltra {
	[CreateAssetMenu (fileName = "Ultra Noise Graph", menuName = "KillMobil/UltraNoise/Noise Graph")]
	public class NoiseNodeGraph : NodeGraph {

		public void OnEnable ()
		{
			//UpdateAllValues();
		}

		[Button]
		public void UpdateAllValues()
		{
			foreach (Node n in nodes)
			{
				if (n is NoiseNodeBase) {
					NoiseNodeBase nnb = n as NoiseNodeBase;
					nnb.UpdateValues(); 
				}
			}
		}

		public float Sample1D(float x)
		{
			if (outPutNoise == null) return -1;
			float v = outPutNoise.Sample1D(x);
			return (v);
		}

		public float Sample2D (float x, float y) {
			if (outPutNoise == null) return -1;
			float v = outPutNoise.Sample2D (x, y);
			return (v);
		}

		public float Sample3D (float x, float y, float z) {
			if (outPutNoise == null) return -1;
			float v = outPutNoise.Sample3D (x, y, z);
			return (v);
		}

		[SerializeField]
		public NoiseNodeOutPut _myNoiseNodeOutPut;
		public NoiseNodeOutPut outPutNoise {
			get {
				if (_myNoiseNodeOutPut == null) {
					foreach (Node n in nodes) {
						if (n is NoiseNodeOutPut) {
							_myNoiseNodeOutPut = ((NoiseNodeOutPut) n);
						}
					}
				}
				return _myNoiseNodeOutPut;
			}
		}

	}
}